using System;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyScript : MonoBehaviour, IEnemy,IDamagable
{
    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack,
        Death
    }

    [Header("States")]
    public EnemyState currentState;
    private bool alreadyAttacked;

    [Header("Patrolling Transforms")]
    [SerializeField] private Transform patrolPointA;
    [SerializeField] private Transform patrolPointB;
    private Transform currentTarget;

    [Header("Navmesh Agent")]
    private NavMeshAgent basicEnemyAgent;

    [Header("player Components")]
    [SerializeField] private Transform player;

    [Header("Ranges")]
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float attackRange = 2f;

    [Header("Enemy Data", order = 1)]
    [SerializeField] private float health = 100;
    [SerializeField] private float headShotDamage = 100;

    private Animator animator;
    public bool isDeath;
    public bool isHeadshot;
    public GameObject Head;
    public GameObject SeprateHead;
    public Transform headTransform;

    private EnemyVision enemyVision;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        basicEnemyAgent = GetComponent<NavMeshAgent>();
        if (basicEnemyAgent == null)
        {
            Debug.LogError("NavMeshAgent is missing on this GameObject!");
        }
    }

    void Start()
    {
        enemyVision = FindAnyObjectByType<EnemyVision>();
        isDeath = false;

        currentState = EnemyState.Patrol;
        currentTarget = patrolPointA;

        //Time.timeScale = 0.5f;
    }

    void Update()
    {
        UpdateState();
        if (health < 0)
        {
            Death();
        }

    }

    void UpdateState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Update state based on distance to player
        if (distanceToPlayer <= attackRange && enemyVision.isVisible)
        {
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer <= chaseRange && enemyVision.isVisible)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }

        if (isDeath)
        {
            currentState = EnemyState.Death;
        }
        // Switch between behaviors
        switch (currentState)
        {
            case EnemyState.Patrol:
                PlayWalk();
                Patrol();
                break;

            case EnemyState.Chase:
                PlayChase();
                Chase();
                break;

            case EnemyState.Attack:
                PlayAttack();
                Attack();
                break;

            case EnemyState.Death:
                Death();
                break;

            default:
                break;
        }
    }

    public void Patrol()
    {
        if (Vector3.Distance(transform.position, currentTarget.position) <= 5f)
        {
            if (currentTarget == patrolPointA)
            {
                currentTarget = patrolPointB;
            }
            else { currentTarget = patrolPointA; }
        }

        basicEnemyAgent.SetDestination(currentTarget.position);
    }

    public void Chase()
    {
        transform.LookAt(player.position);
        basicEnemyAgent.SetDestination(player.position);
    }

    public void Attack()
    {
        basicEnemyAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Debug.Log("Enemy attacks the player!");

            Invoke(nameof(ResetAttack), 5f);
        }
    }

    public void Death()
    {
        isDeath = true;

        animator.SetTrigger("isDeath");
        if (isHeadshot)
        {
            Head.SetActive(false);
            GameObject head = Instantiate(SeprateHead, headTransform.position, Quaternion.identity);
            isHeadshot = false;

            Destroy(gameObject, 5f);
            Destroy(head, 5f);
        }
        Destroy(gameObject, 5f);
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void PlayWalk()  //Plays Walk Animation Smoothly
    {
        animator.SetBool("isWalk", true);

        animator.SetBool("isChase", false); 

        animator.SetBool("isAttack", false);
    }
    void PlayChase() //Plays Chase Animation Smoothly
    {
        animator.SetBool("isChase", true);

        animator.SetBool("isAttack", false);

        animator.SetBool("isWalk", false); 

    }
    void PlayAttack() //Plays Attack Animation Smoothly
    {

        animator.SetBool("isAttack", true); 

        animator.SetBool("isChase", false);

        animator.SetBool("isWalk", false); 

    }
    public void Damage(float damage, Collider hitCollider)
    {
        if (hitCollider == null)
        {
            return;
        }
        Debug.Log("Enemy hurt");
        if(hitCollider.gameObject == Head)
        {
            isDeath = true;
            isHeadshot = true;
            health -= damage * headShotDamage;
            Death();
            Debug.Log("Enemy  head hurt");
        }
        else
        {
            health -= damage;
            //PlayHurt();
        }
    }
    //void PlayHurt()
    //{

    //    animator.SetTrigger("isWalk"); //Plays Walk Animation 

    //    animator.SetTrigger("isChase");//Plays Chase Animation 

    //    animator.SetTrigger("isAttack"); //Plays Attack Animation 
    //}
}
