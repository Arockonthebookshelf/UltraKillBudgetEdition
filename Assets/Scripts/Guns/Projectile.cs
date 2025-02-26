using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;  // Time before the projectile self-destructs if no collision occurs
    private float timeAlive;
    public float damage;

    void Start()
    {
        timeAlive = 0f;
    }

    void Update()
    {
        // Increment time since projectile was created
        timeAlive += Time.deltaTime;

        // If projectile has lived longer than the lifetime, destroy it
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object hit has an IDamagable interface
        IDamagable damageable = collision.gameObject.GetComponent<IDamagable>();

        if (damageable != null)
        {
            // Call the TakeDamage method on the enemy
        }

        // Destroy the projectile after it hits anything ground
        if (collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

