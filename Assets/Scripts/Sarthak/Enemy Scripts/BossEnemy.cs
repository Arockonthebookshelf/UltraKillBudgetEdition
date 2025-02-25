using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : BaseEnemy
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;

    [Tooltip("Range within which the enemy will begin attacking (shooting).")]
    public float shootingRange;

    [Tooltip("Range for melee attacks (takes precedence over shooting if in range).")]
    public float meleeRange;

    [Tooltip("Range for melee attacks (takes precedence over shooting if in range).")]
    public float missleRange;

    [Header("RocketLauncher Settings")]
    public GameObject RocketPrefab;
    public Transform rocketShootPoint;
    public float missileSpeed = 20f;

    private bool alreadyAttacked = false;

    // Override the attack range so that the shooter enemy can attack from its shooting range.
    protected override float AttackStateRange => shootingRange;
    protected override void Attack()
    {
        // Stop movement during the attack.
        agent.SetDestination(transform.position);

        // Smoothly face the player.
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }

        // Use the distance computed in the base class.
        bool isInMeleeRange = distanceToPlayer <= meleeRange;
        bool isInShootingRange = distanceToPlayer <= shootingRange;
        bool isInMissileRange = distanceToPlayer <= missleRange;

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;

            // Smoothly rotate toward the player.
            Vector3 direction = (player.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // Prioritize melee if the player is close enough.
            if (isInMeleeRange)
            {
                Debug.Log("ShooterEnemy melee attacks the player!");
                // TODO: Implement melee attack damage or effects here.
            }
            else if (isInShootingRange)
            {
                Debug.Log("ShooterEnemy shoots the player!");
                GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
                Destroy(projectile, 2f);


                // Reset attack after a cooldown.
                Invoke(nameof(ResetAttack), 0.5f);
            }
            else if (isInMissileRange)
            {
                Debug.Log("TankEnemy shoots the player!");
                StartCoroutine(ShootRockets());
                Invoke(nameof(ResetAttack), 8f); // Cooldown before next attack
            }
        }
    }
    IEnumerator ShootRockets()
    {
        int rocketLaunched = 3;
        for (int i = 1; i < rocketLaunched + 1; i++)
        {
            GameObject projectile = Instantiate(RocketPrefab, rocketShootPoint.position, rocketShootPoint.rotation);
            var p = projectile.GetComponent<EnemyMissile>();
            p.missileSpeed = missileSpeed - (i * 2);
            Destroy(projectile, 5f);
            yield return new WaitForSeconds(0.5f);
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
