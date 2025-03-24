using System.Collections;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Enemy Vision Toggles")]
    public bool isVisible;
    [SerializeField] private float rayRangeLength = 20f;

    [Header("Player Detection")]
    private GameObject player;
    [SerializeField] private float rayInterval;
    Vector3 direction;

    public LayerMask enemyHead;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        isVisible = false;
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        while (true)
        {
            RaycastHit hit;
            direction = (player.transform.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, direction, out hit, rayRangeLength, ~enemyHead))
            {
                isVisible = hit.transform.gameObject == player;
                //isVisible = hit.collider.CompareTag("Player");
                //Debug.Log(hit.collider.name);
            }
            else
            {
                isVisible = false;
            }
            Debug.DrawRay(transform.position, direction * rayRangeLength, isVisible ? Color.green : Color.red);

            yield return new WaitForSeconds(rayInterval);
        }
    }
}
