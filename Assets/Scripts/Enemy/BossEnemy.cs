using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : BaseEnemy
{
    public enum AttackState { Melee, Shooting, Missile, Dash}

    [Header("States")]
    [SerializeField] private AttackState currentAtkState;


    [Tooltip("Range for melee attacks")]
    public float meleeRange;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed;
    [Tooltip("Range within which the enemy will begin attacking (shooting).")]
    public float shootingRange;
    public float projectile_Y_Offset;

    [Header("RocketLauncher Settings")]
    public GameObject RocketPrefab;
    public Transform rocketShootPoint;
    public float missileSpeed = 20f;
    [Tooltip("Range for missile attacks.")]
    public float missileRange;


    [Header("Dash Settings")]
    [Tooltip("Range for melee attacks")]
    [SerializeField] private float dashRange;
    [SerializeField] private Transform target;
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeedMultiplier;

    private Rigidbody rb;
    private bool alreadyAttacked = false;
    Vector3 startPos;
    Vector3 endPos;
    public bool isdashing;

    protected override float AttackStateRange => Mathf.Max(shootingRange, meleeRange, missileRange);

    void Awake()
    {
        PreInitialize();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Initialize();

        target = player.transform;
    }

    void Update()
    {
        StateChanges();

        startPos = transform.position;
        endPos = target.position;
    }

    private void PerformAttackStateAction()
    {
        alreadyAttacked = true;

        switch (currentAtkState)
        {
            case AttackState.Melee:
                MeleeATK();
                break;
            case AttackState.Dash:
                DashATK();
                break;

            case AttackState.Shooting:
                ShootingATK();
                break;

            case AttackState.Missile:
                MissileATK();
                break;

        }
    }

    protected override void Attack()
    {
        if (!isdashing)
        agent.SetDestination(transform.position);

        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }

        if (distanceToPlayer <= meleeRange)
        {
            currentAtkState = AttackState.Melee;
        }
        else if (distanceToPlayer <= shootingRange)
        {
            currentAtkState = AttackState.Shooting;
        }
        else if (distanceToPlayer <= missileRange)
        {
            currentAtkState = AttackState.Missile;
        }
        else if (distanceToPlayer <= dashRange)
        {
            currentAtkState = AttackState.Dash;
        }

        if (!alreadyAttacked)
        {
            PerformAttackStateAction();
        }
    }

    private void MeleeATK()
    {
        Debug.Log("BossEnemy melee attacks the player!");
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void DashATK()
    {
        StartCoroutine(PerformDashAtk());
        Invoke(nameof(ResetAttack), 2f);
    }

    private void ShootingATK()
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool("Projectile", shootPoint.position, shootPoint.rotation);

        Vector3 targetPosition = player.position - new Vector3(0, projectile_Y_Offset, 0);
        Vector3 direction = (targetPosition - transform.position).normalized;
        projectile.GetComponent<Rigidbody>().AddForce(direction * (projectileSpeed * 10));
        //Destroy(projectile, 2f);

        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void MissileATK()
    {
        Debug.Log("BossEnemy launches missiles at the player!");
        StartCoroutine(ShootRockets());
        Invoke(nameof(ResetAttack), 8f);
    }

    IEnumerator ShootRockets()
    {
        int rocketLaunched = 3;
        for (int i = 1; i <= rocketLaunched; i++)
        {
            GameObject projectile = ObjectPooler.Instance.SpawnFromPool("Rockets", shootPoint.position, shootPoint.rotation);
            var p = projectile.GetComponent<EnemyMissile>();
            p.missileSpeed = missileSpeed - (i * 2);
            //Destroy(projectile, 5f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator PerformDashAtk()
    {
        // Capture the player's position at the start of the dash
        Vector3 dashTarget = player.position;

        // Capture the original speed and boost for the dash
        float originalSpeed = agent.speed;

        agent.speed = originalSpeed * dashSpeedMultiplier;

        // Set the destination to the captured dash target
        agent.SetDestination(dashTarget);

        isdashing = true;

        float elapsed = 0f;
        float threshold = 0.5f; // Distance threshold to consider the target reached

        // Continue dashing until dashDuration elapses or the enemy is close to the dash target
        while (elapsed < dashDuration && Vector3.Distance(transform.position, dashTarget) > threshold)
        {
            // Continuously update rotation so the enemy faces the dash direction
            Vector3 direction = (dashTarget - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                // Using Time.deltaTime here helps smooth rotation over the dash
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Once dash is complete, ensure we set the position exactly to dashTarget
        agent.SetDestination(dashTarget);

        // Restore the original agent speed and mark dashing as finished
        agent.speed = originalSpeed;
        isdashing = false;
    }


    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, dashRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, missileRange);
    }
}
