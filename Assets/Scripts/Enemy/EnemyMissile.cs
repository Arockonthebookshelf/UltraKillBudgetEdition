using UnityEngine;
using UnityEngine.ProBuilder;

public class EnemyMissile : MonoBehaviour
{
    public float missileSpeed = 10f;
    public float missileRotationSpeed = 5f;

    private Vector3 direction;
    private GameObject player;
    private Quaternion initialRotationOffset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialRotationOffset = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
        direction = (player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction) * initialRotationOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * missileRotationSpeed);
        transform.position += transform.up * missileSpeed * Time.deltaTime;
    }
}
