using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemy : BaseEnemy
{
    [Header("Projectile Reference")]
    public GameObject projectilePrefab;
    public Transform shootPoint;

    private bool alreadyAttacked=false;

    protected override void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            transform.LookAt(player);
            Debug.Log("Enemy attacks the player!");

            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Destroy(projectile, 2f);

            Invoke(nameof(ResetAttack), 2f);
        }
    }
    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
