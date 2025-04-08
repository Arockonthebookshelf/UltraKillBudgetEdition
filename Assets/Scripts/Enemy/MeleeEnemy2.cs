using UnityEngine;

public class MeleeEnemy2 : BaseEnemy2
{
    [Header("Melee Enemy Stats")]
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float attackMoveSpeed = 10f;
    [SerializeField] private float damageDelay = 0.5f;

    private float currentSpeed;
    void Awake()
    {
        PreInitialize();
    }
    private void Start()
    {
        Initialize();
        agent.speed = attackMoveSpeed;
    }
    void Update()
    {
        StateChanges();
        if (currentState != EnemyState.Move)
        {
            Move();
        }

        if(agent.speed < moveSpeed)
        {
            agent.speed += 10f * Time.deltaTime;
            Mathf.Clamp(agent.speed, attackMoveSpeed, moveSpeed);
        }

        currentSpeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", currentSpeed/moveSpeed);
    }
    private bool alreadyAttacked = false;

    protected override void Attack()
    {
        bool isInMeleeRange = distanceToPlayer <= attackRange;
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            PlayAttack();
            Invoke(nameof(DealDamage), damageDelay);
        }
    }

    public void DealDamage()
    {
        Invoke(nameof(ResetAttack), attackRate);

        if (distanceToPlayer <= attackRange)
        {
            player.GetComponent<IDamagable>().Damage(attackDamage, null);
            agent.speed = attackMoveSpeed;
        }
    }

    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
