using UnityEngine;

public class MeleeEnemy2 : BaseEnemy2
{
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float attackDamage = 10f;
    void Awake()
    {
        PreInitialize();
    }
    private void Start()
    {
        Initialize();
    }
    void Update()
    {
        StateChanges(); 
    }
    private bool alreadyAttacked = false;

    protected override void Attack()
    {
        bool isInMeleeRange = distanceToPlayer <= attackRange;
        if (!alreadyAttacked)
        {
            PlayAttack();
            alreadyAttacked = true;
             
            if (isInMeleeRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                    if (damagable != null ) 
                    {
                        damagable.Damage(attackDamage, hit.collider);
                    }
                }
            }
            Invoke(nameof(ResetAttack), attackRate);
        }
    }

    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Draw Shooting Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
