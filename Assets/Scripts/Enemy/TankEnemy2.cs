using System.Collections;
using UnityEngine;

public class TankEnemy2 : BaseEnemy2
{
    public enum AttackState { Melee, Missile }

    [Header("States")]
    [SerializeField] private AttackState currentAtkState;

    [Header("RocketLauncher Settings")]
    public GameObject RocketPrefab;
    [Tooltip("Range within which the enemy will begin attacking (RocketShooting).")]
    public float MissileRange;
    public float missileSpeed = 20f;
    public Transform shootPoint;

    [Header("Melee Settings")]
    [Tooltip("Range within which the enemy will begin attacking (Melee).")]
    [SerializeField] public float meleeRange;

    private Rigidbody rb;
    private bool alreadyAttacked = false;
    Vector3 startPos;
    Vector3 endPos;

    void Awake()
    {
        PreInitialize();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Initialize();
    }

    void Update()
    {
        StateChanges();
    }
    private void PerformAttackStateAction()
    {
        alreadyAttacked = true;

        switch (currentAtkState)
        {
            case AttackState.Melee:
                PlayMeleeATK();
                MeleeATK();
                break;

            case AttackState.Missile:
                PlayMissileATK();
                MissileATK();
                break;

            default:
                break;
        }
    }
    #region Attacks
    private void MeleeATK()
    {
        Debug.Log("TankEnemy melee attacks the player!");
        agent.SetDestination(player.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            Debug.Log(hit.collider.name);
            IDamagable damagable = hit.collider.GetComponent<IDamagable>();

            if (damagable != null)
            {
                Debug.Log("TankEnemy melee attacks the player!");
                damagable.Damage(20f, hit.collider);
            }
        }
        Invoke(nameof(ResetAttack), 1f);
    }
    private void MissileATK()
    {
        StartCoroutine(ShootRockets());
        Invoke(nameof(ResetAttack), 8f); // Cooldown before next attack
    }
    #endregion

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

        // Select attack based on distance
        if (distanceToPlayer <= meleeRange)
        {
            currentAtkState = AttackState.Melee;
        }
        else // Use missile attack if not in melee range
        {
            currentAtkState = AttackState.Missile;
        }
        if (!alreadyAttacked)
        {
            PerformAttackStateAction();
        }
    }
    IEnumerator ShootRockets()
    {
        int rocketLaunched = 3;
        for (int i = 1; i <= rocketLaunched; i++)
        {
            GameObject projectile = ObjectPooler.Instance.SpawnFromPool("Rockets", shootPoint.position, shootPoint.rotation);
            var p = projectile.GetComponent<EnemyMissile>();
            p.missileSpeed = missileSpeed - (i * 2);
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    #region Attack Animations
    private void PlayMeleeATK()
    {
        animator.SetBool("isAttack", true);
    }

    private void PlayMissileATK()
    {
        animator.SetBool("isAttack", true);
    }
    #endregion
}
