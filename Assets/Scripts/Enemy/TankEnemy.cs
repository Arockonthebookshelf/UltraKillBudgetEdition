using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TankEnemy : BaseEnemy
{
    public enum AttackState {Melee, Missile, Jump}

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

    [Header("Jump Settings")]
    [Tooltip("Range within which the enemy will begin attacking (Melee).")]
    [SerializeField] private float JumpRange;
    [SerializeField] private Transform target;
    [SerializeField] float maxJumpHeight;
    [SerializeField] float jumpDuration;

    private Rigidbody rb;
    private bool alreadyAttacked = false;
    Vector3 startPos;
    Vector3 endPos;
    public bool isjumping;

    // Override the attack range so that the shooter enemy can attack from its shooting range.
    protected override float AttackStateRange => MissileRange;

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
                PlayMeleeATK();
                MeleeATK();
                break;

            case AttackState.Missile:
                PlayMissileATK();
                MissileATK();
                break;

            case AttackState.Jump:
                PlayJumpATK();
                JumpATK();
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
        // TODO: Implement melee attack damage or effects here.
        Invoke(nameof(ResetAttack), .5f);
    }
    private void JumpATK()
    {
        StartCoroutine(PerformJumpAtk(startPos, endPos, jumpDuration, maxJumpHeight));
        Invoke(nameof(ResetAttack), 2f);
    }

    private void MissileATK()
    {
        StartCoroutine(ShootRockets());
        Invoke(nameof(ResetAttack), 8f); // Cooldown before next attack
    }

    #endregion

    #region Attack Animations
    private void PlayMeleeATK()
    {
        animator.SetBool("isAttack", true);
    }
    private void PlayJumpATK()
    {
        animator.SetBool("isJump", true);
        animator.SetBool("isAttack", false);
    }
    private void PlayMissileATK()
    {
        
        animator.SetBool("isAttack", true);
        animator.SetBool("isJump", false);
    }

    #endregion
    protected override void Attack()
    {
        if(isjumping)
            return;
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

        if (distanceToPlayer <= meleeRange)
        {
            currentAtkState = AttackState.Melee;
        }
        else if (distanceToPlayer <= JumpRange)
        {
            currentAtkState = AttackState.Jump;
        }
        else //if (distanceToPlayer <= MissileRange)
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
        if (isjumping)
            yield break;
        int rocketLaunched = 3;
        for (int i = 1; i < rocketLaunched + 1; i++)
        {
            GameObject projectile = ObjectPooler.Instance.SpawnFromPool("Rockets", shootPoint.position, shootPoint.rotation);
            var p = projectile.GetComponent<EnemyMissile>();
            p.missileSpeed = missileSpeed - (i * 2);
            //print(p.missileSpeed);
            //Destroy(projectile, 5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator PerformJumpAtk(Vector3 startPos, Vector3 endPos, float duration, float maxHeight)
    {
        isjumping = true;
        agent.enabled = false;

        float elapsed = 0;
        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Calculate the horizontal (X, Z) position using linear interpolation
            // between the start and end positions.
            Vector3 horizontalMove = Vector3.Lerp(startPos, endPos, t);

            // Calculate the vertical offset (Y) using a parabolic function:
            // This formula produces a curve that starts and ends at 0, and peaks at maxHeight when t = 0.5.
            float yOffset = 4 * maxHeight * t * (1 - t);


            Vector3 newPos = new Vector3(horizontalMove.x, horizontalMove.y + yOffset, horizontalMove.z);

            // which respects physics and collision detection.
            rb.MovePosition(newPos);

            // Increment the elapsed time by the time passed since the last frame.
            elapsed += Time.deltaTime;
            
            // Wait until the next FixedUpdate frame for physics calculations.
            yield return new WaitForFixedUpdate();

            
            Debug.Log("jump exit");
        }
        rb.MovePosition(endPos);
        isjumping = false;
        agent.enabled = true;
    }
    protected override void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmos()
    {
        // Only draw gizmos when the game is playing.
        if (!Application.isPlaying)
            return;

        // Draw Missile Range as a red wire sphere.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MissileRange);

        // Draw Melee Range as a yellow wire sphere.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        // Draw Jump Range as a green wire sphere.
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, JumpRange);

        // If no target is set, exit early.
        if (target == null)
            return;

        // Draw the jump trajectory.
        // Use the current position as the start and the target's position as the end.
        Vector3 startPos = transform.position;
        Vector3 endPos = target.position;
        int resolution = 20;  // Number of segments in the trajectory curve.
        Gizmos.color = Color.red;  // Set the color for the trajectory.

        // Store the previous point to draw continuous line segments.
        Vector3 previousPoint = startPos;

        // Loop through points along the trajectory.
        for (int i = 1; i <= resolution; i++)
        {
            // Calculate a normalized value t.
            float t = i / (float)resolution;
            // Compute horizontal interpolation.
            Vector3 horizontalMove = Vector3.Lerp(startPos, endPos, t);
            // Compute the vertical offset using the same parabolic formula as in the coroutine.
            float yOffset = 4 * maxJumpHeight * t * (1 - t);
            // Calculate the current point along the arc.
            Vector3 currentPoint = new Vector3(horizontalMove.x, horizontalMove.y + yOffset, horizontalMove.z);
            // Draw a line from the previous point to the current point.
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }
}
