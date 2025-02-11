using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase, Attack, Death }

    [Header("States")]
    protected EnemyState currentState;

    [Header("NavMesh Agent")]
    protected NavMeshAgent agent;

    [Header("Player Reference")]
    [SerializeField] protected Transform player;

    [Header("Patrol Points")]
    [SerializeField] protected Transform patrolPointA;
    [SerializeField] protected Transform patrolPointB;
    protected Transform currentTarget;

    [Header("Detection Ranges")]
    [SerializeField] protected float chaseRange = 10f;
    [SerializeField] protected float attackRange = 2f;
    [SerializeField] protected float ShootRange = 10f;

    protected Animator animator;
    protected bool isDead = false;

    [SerializeField] protected bool isDeath;
    [SerializeField] protected bool isHeadshot;
    [SerializeField] protected GameObject Head;
    [SerializeField] protected GameObject SeprateHead;
    [SerializeField] protected Transform headTransform;

    private EnemyVision enemyVision;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyVision = FindAnyObjectByType<EnemyVision>();
    }

    void Start()
    {
        currentState = EnemyState.Patrol;
        currentTarget = patrolPointA;
    }

    protected virtual void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && enemyVision.isVisible
            || distanceToPlayer < ShootRange && enemyVision.isVisible)
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
                break;

            default:
                break;
        }
    }
    protected virtual void Patrol()
    {
        if (Vector3.Distance(transform.position, currentTarget.position) < 1f)
        {
            currentTarget = (currentTarget == patrolPointA) ? patrolPointB : patrolPointA;
        }
        agent.SetDestination(currentTarget.position);
    }

    protected virtual void Chase()
    {
        agent.SetDestination(player.position);
    }

    //Attack
    protected abstract void Attack();  // Force each enemy to define their attack behavior
    protected abstract void ResetAttack();
    //Attack

    protected virtual void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        agent.isStopped = true;
        Destroy(gameObject, 3f);
    }



    //Animations
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
    //Aniamtions
}
