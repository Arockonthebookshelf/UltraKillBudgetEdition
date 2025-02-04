using System.Collections;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Enemy Vision Toggles")]
    public bool isVisible;
    [SerializeField] private float rayRangeLength = 20f;

    [Header("Player Detection")]
    [SerializeField] private GameObject player;
    Vector3 direction;

    public LayerMask enemyHead;

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
            }
            else
            {
                isVisible = false;
            }
            Debug.Log(hit.collider.name);
            Debug.DrawRay(transform.position, direction * rayRangeLength, isVisible ? Color.green : Color.red);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
