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
    [SerializeField] private float meleeDamage = 20f;

    private bool alreadyAttacked = false;

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
        animator.SetFloat("Speed", agent.velocity.magnitude);
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
                MissileATK();
                break;

            default:
                break;
        }
    }
    #region Attacks
    private void MeleeATK()
    {
        agent.SetDestination(player.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            Debug.Log(hit.collider.name);
            IDamagable damagable = hit.collider.GetComponent<IDamagable>();

            if (damagable != null)
            {
                damagable.Damage(meleeDamage, hit.collider);
            }
        }
        Invoke(nameof(ResetAttack), 1f);
    }
    private void MissileATK()
    {
        StartCoroutine(ShootRockets());
        Invoke(nameof(ResetAttack), 5f);
    }
    #endregion

    protected override void Attack()
    {

        agent.SetDestination(transform.position);

        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }

        if (distanceToPlayer <= meleeRange)
        {
            currentAtkState = AttackState.Melee;
        }
        else 
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
        animator.SetTrigger("Attack");
    }
    #endregion
}
