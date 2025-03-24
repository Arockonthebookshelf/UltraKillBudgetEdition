using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemy : BaseEnemy
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

    // Override the attack range so that the shooter enemy can attack from its shooting range.
    protected override float AttackStateRange => shootingRange;
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
        // Stop movement during the attack.
        agent.SetDestination(transform.position);

        // Smoothly face the player.
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }

        // Use the distance computed in the base class.
        bool isInMeleeRange = distanceToPlayer <= meleeRange;
        bool isInShootingRange = distanceToPlayer <= shootingRange;

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;

            //// Smoothly rotate toward the player.
            //direction = (player.position - transform.position).normalized;
            //if (direction != Vector3.zero)
            //{
            //    Quaternion lookRotation = Quaternion.LookRotation(direction);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            //}

            // Prioritize melee if the player is close enough.
            if (isInMeleeRange)
            {
                PlayAttack();
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
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                PlayAttack();
                GameObject projectile = ObjectPooler.Instance.SpawnFromPool("Projectiles", shootPoint.position, shootPoint.rotation);

                Vector3 targetPosition = player.position - new Vector3(0, yOffset, 0);
                Vector3 d = (targetPosition - transform.position).normalized;

                projectile.GetComponent<Rigidbody>().AddForce(d * (projectileSpeed), ForceMode.Impulse);
            }

            // Reset attack after a cooldown.
            Invoke(nameof(ResetAttack), 2f);
        }
    }

    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        // Ensure gizmos are always visible, even when the object is not selected
        if (!Application.isPlaying) return; // Optional: Only draw gizmos during play mode

        // Draw Shooting Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);

        // Draw Melee Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}
