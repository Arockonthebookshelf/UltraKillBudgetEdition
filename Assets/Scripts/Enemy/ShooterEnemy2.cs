using UnityEngine;

public class ShooterEnemy2 : BaseEnemy2
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed;

    [Tooltip("Range within which the enemy will begin attacking (shooting).")]
    public float shootingRange;

    [Tooltip("Range for melee attacks (takes precedence over shooting if in range).")]
    public float meleeRange;

    private bool alreadyAttacked = false;

    public float yOffset;

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

    protected override void Attack()
    {
        agent.SetDestination(transform.position);

        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }

        bool isInMeleeRange = distanceToPlayer <= meleeRange;
        bool isInShootingRange = distanceToPlayer <= shootingRange;

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;

            if (isInMeleeRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    Debug.Log(hit.collider.name);
                    IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        Debug.Log("ShooterEnemy melee attacks the player!");
                        damagable.Damage(20f, hit.collider);
                    }
                }
            }
            else if (isInShootingRange)
            {
                Debug.Log("Eneter shooting range");
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                GameObject projectile = ObjectPooler.Instance.SpawnFromPool("Projectiles", shootPoint.position, shootPoint.rotation);

                Vector3 targetPosition = player.position - new Vector3(0, yOffset, 0);
                Vector3 d = (targetPosition - transform.position).normalized;

                projectile.GetComponent<Rigidbody>().AddForce(d * (projectileSpeed), ForceMode.Impulse);
            }

            Invoke(nameof(ResetAttack), 2f);
        }
    }

    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // Draw gizmos for melee and shooting ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
