using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy2 : MonoBehaviour, IDamagable
{
    public enum EnemyState { Move, Attack, Death }

    [Header("States")]
    [SerializeField] protected EnemyState currentState;

    [Header("NavMesh Agent")]
    protected NavMeshAgent agent;

    [Header("Player Reference")]
    protected Transform player;

    [Header("Ranges")]
    [SerializeField] protected float attackRange = 2f;
    [SerializeField] protected float secondaryAttackRange = 10f;

    [Header("Enemy Health")]
    [SerializeField] protected float enemyHealth;

    [Header("Item Drop")]
    public GameObject itemDropper;

    [Header("Enemy Particle and Decal")]
    [SerializeField] GameObject bloodPS;

    protected Animator animator;
    protected bool isDead = false;

    protected float distanceToPlayer;

    protected Vector3 direction;

    protected virtual float AttackStateRange => attackRange;

    protected void PreInitialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected void Initialize()
    {
        currentState = EnemyState.Move;
    }

    protected void StateChanges()
    {
        if (isDead) return;

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
        else
        {
            currentState = EnemyState.Move;
        }

        if (enemyHealth <= 0)
        {
            currentState = EnemyState.Death;
        }

        PerformStateAction();
    }
    private void PerformStateAction()
    {
        switch (currentState)
        {
            case EnemyState.Move:
                Move();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Death:
                Die();
                break;
        }
    }

    protected virtual void Move()
    {
        {
            agent.SetDestination(player.position);
        }
    }

    protected abstract void Attack();
    protected abstract void ResetAttack();

    public void Damage(float damage, Collider collider)
    {
        enemyHealth = (enemyHealth - damage);
        Instantiate(bloodPS, transform.position, Quaternion.identity);
    }

    protected virtual void Die()
    {
        isDead = true;
        agent.isStopped = true;
        Instantiate(itemDropper, transform.position, Quaternion.identity);
        Instantiate(bloodPS, transform.position, Quaternion.identity);
        Invoke("DestroyEnemy", 0.1f);
    }

    #region Aniamtions Function
    protected virtual void Moving()
    {
        animator.SetBool("Moving", true);
    }
    protected virtual void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }
    #endregion

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
