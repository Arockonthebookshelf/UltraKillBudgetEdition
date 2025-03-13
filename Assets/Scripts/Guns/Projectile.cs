using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f; 
    [SerializeField] float damage;

    private Shotgun shotgun;
    TrailRenderer trail;
    void Awake()
    {
        shotgun = FindFirstObjectByType<Shotgun>();
        trail = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        Invoke("DisableProjectile", lifetime);
    }

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("DisableProjectile", lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damageable = collision.gameObject.GetComponent<IDamagable>();

        if (damageable != null)
        {
            damageable.Damage(damage, collision.collider);
        }

        DisableProjectile();
    }

    private void DisableProjectile()
    {
        trail.Clear();
        shotgun.ReturnBulletToPool(gameObject);
    }
}

