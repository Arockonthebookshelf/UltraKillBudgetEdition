using PrometheanUprising.SoundManager;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    // Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;
    [SerializeField] private string bulletTag = "RocketLauncher Projectiles";
    [SerializeField] GameObject bloodPrefab;

    // Stats
    public bool useGravity;

    // Damage
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    // Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;
    private bool hasExploded = false;

    private int collisions;
    private float currentLifetime;

    private void OnEnable()
    {
        Setup();
    }

    private void Update()
    {
        if (collisions > maxCollisions) Explode();

        // Countdown lifetime
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0) Explode();
    }

    private void Explode()
    {
        if (hasExploded) return; // Prevent multiple explosions
        hasExploded = true;

        SoundManager.PlaySound(SoundType.RPG_BURST);

        // Instantiate explosion effect
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        // Check for enemies in explosion radius
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        foreach (Collider enemy in enemies)
        {
            IDamagable damagable = enemy.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(explosionDamage, enemy);
                Instantiate(bloodPrefab, enemy.transform.position, Quaternion.identity);
            }
        }

        // Reset & return rocket to object pool
        Invoke(nameof(ReturnToPool), 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ignore collisions with other bullets
        if (collision.collider.CompareTag("Bullet")) return;

        // Increase collision count
        collisions++;

        // Explode if hitting an enemy directly
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch)
        {
            Explode();
        }
    }

    private void Setup()
    {
        rb.useGravity = useGravity;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        hasExploded = false;
        collisions = 0;
        currentLifetime = maxLifetime;
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        ObjectPooler.Instance.EnqueObject(bulletTag, gameObject);
    }

    /// Visualize explosion range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
