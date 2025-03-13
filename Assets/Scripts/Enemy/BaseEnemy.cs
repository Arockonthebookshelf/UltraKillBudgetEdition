using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.ParticleSystem;

public abstract class BaseEnemy : MonoBehaviour, IDamagable
{
    public enum EnemyState { Patrol, Chase, Attack, Death }

    [Header("States")]
    [SerializeField] protected EnemyState currentState;

    [Header("NavMesh Agent")]
    protected NavMeshAgent agent;

    [Header("Player Reference")]
    [SerializeField] protected Transform player;    

    [Header("Patrol Points")]
    [SerializeField] public Transform patrolPointA;
    [SerializeField] public Transform patrolPointB;
    protected Transform currentTarget;

    [Header("Detection Ranges")]
    [SerializeField] protected float chaseRange = 10f;
    [SerializeField] protected float attackRange = 2f;

    protected Animator animator;
    protected bool isDead = false;

    [SerializeField] protected bool isDeath;
    [SerializeField] protected bool isHeadshot;
    [SerializeField] protected GameObject Head;
    [SerializeField] protected GameObject SeprateHead;
    [SerializeField] protected Transform headTransform;

    [Header("Enemy Health")]
    [SerializeField] protected float enemyHealth;
    [SerializeField] public LayerMask enemyHead;

    [Header("Item Drop")]
    public GameObject itemDropper;


    [Header("Enemy Particle and Decal")]
    [SerializeField] GameObject singleShotParticle;
    [SerializeField] GameObject DeathParticle;
    [SerializeField] GameObject enemyDeacal;

    private EnemyVision enemyVision;
    protected float distanceToPlayer;
    private TankEnemy TankEnemy;
    private BossEnemy bossEnemy;

    public int resetHealth;

    // Virtual properties so derived enemies can override detection thresholds.
    protected virtual float AttackStateRange => attackRange;
    protected virtual float ChaseStateRange => chaseRange;

    protected void PreInitialize()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        enemyVision = GetComponent<EnemyVision>();
        TankEnemy = GetComponent<TankEnemy>();
        bossEnemy = GetComponent<BossEnemy>();
    }

    protected void Initialize()
    {
        currentState = EnemyState.Patrol;
        currentTarget = patrolPointA;
    }

    protected void StateChanges()
    {
        if (isDead) return;

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && enemyVision.isVisible)
        {
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer <= chaseRange && enemyVision.isVisible)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }

        if(enemyHealth <= 0)
        {
            currentState = EnemyState.Death;
        }

        PerformStateAction();
    }
    private void PerformStateAction()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                PlayWalk();
                Patrol();
                break;

            case EnemyState.Chase:
                PlayChase();
                Chase();
                break;

            case EnemyState.Attack:
                PlayAttack();
                Attack();
                break;

            case EnemyState.Death:
                Die();
                PlayDeath();
                break;

            default:
                break;
        }
    }
    protected virtual void Patrol()
    {
        {
            if (Vector3.Distance(transform.position, currentTarget.position) < 2f)
            {
                currentTarget = (currentTarget == patrolPointA) ? patrolPointB : patrolPointA;
            }
            agent.SetDestination(currentTarget.position);
            agent.speed = 2;
        }
        
    }

    protected virtual void Chase()
    {
        if (!bossEnemy.isdashing)
        { // Smoothly rotate toward the player.
            Vector3 direction = (player.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
            agent.SetDestination(player.position);
            agent.speed = 10;
        }
    }
    public void Reset()
    {
        //Debug.Log("Reset");
        isDead = false;
        enemyHealth = resetHealth;
        Initialize();
    }

    //Attack
    protected abstract void Attack();  // Force each enemy to define their attack behavior
    protected abstract void ResetAttack();
    //Attack

    public void Damage(float damage, Collider collider)
    {
        enemyHealth = (enemyHealth - damage);
        Instantiate(singleShotParticle, transform.position, Quaternion.identity);
    }
    protected virtual void Die()
    {
        isDead = true;
        agent.isStopped = true;
        //Debug.Log("Dead");
    }

    #region Aniamtions Function
    protected virtual void PlayWalk()  //Plays Walk Animation Smoothly
    {
        animator.SetBool("isWalk", true);

        animator.SetBool("isChase", false);

        animator.SetBool("isAttack", false);
    }
    protected virtual void PlayChase() //Plays Chase Animation Smoothly
    {
        animator.SetBool("isChase", true);

        animator.SetBool("isAttack", false);

        animator.SetBool("isWalk", false);

    }
    protected virtual void PlayAttack() //Plays Attack Animation Smoothly
    {

        animator.SetBool("isAttack", true);

        animator.SetBool("isChase", false);

        animator.SetBool("isWalk", false);

    }
    protected virtual void PlayDeath()
    {
        animator.SetBool("isDeath", true);
        Instantiate(itemDropper, transform.position, Quaternion.identity);
        Instantiate(enemyDeacal, transform.position, Quaternion.identity);
        GameObject particle = Instantiate(DeathParticle, transform.position, Quaternion.identity);
        Destroy(particle, 0.5f);
        Invoke("DestroyEnemy", 2f);
        //gameObject.SetActive(false);
        //Destroy(gameObject, 3f);
        //Invoke("gameObject.SetActive(false)", 2f);
    }
    #endregion

    private void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

        private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
