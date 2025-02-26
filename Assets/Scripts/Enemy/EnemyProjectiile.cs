using UnityEngine;

public class EnemyProjectiile : MonoBehaviour
{
    Rigidbody body;
    public float projectileSpeed;
    void Start()
    {
      body = GetComponent<Rigidbody>(); 
    }

    void FixedUpdate()
    {
       body.AddForce(transform.forward * projectileSpeed);
    }
}
