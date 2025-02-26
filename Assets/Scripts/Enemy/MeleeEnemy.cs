using UnityEngine;

public class MeleeEnemy : BaseEnemy 
{
    private bool alreadyAttacked = false;

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
        bool isInMeleeRange = distanceToPlayer <= attackRange;
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
                Debug.Log("MeleeEnemy melee attacks the player!");

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
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Draw Melee Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
