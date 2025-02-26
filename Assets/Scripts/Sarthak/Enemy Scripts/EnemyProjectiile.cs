using UnityEngine;

public class EnemyProjectiile : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public float projectileSpeed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }
}
