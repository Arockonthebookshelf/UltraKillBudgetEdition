using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f; 
    [SerializeField] float damage;

    private Shotgun shotgun;
    TrailRenderer trail;

    void Start()
    {
        shotgun = FindFirstObjectByType<Shotgun>();
        trail = GetComponent<TrailRenderer>();
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
        if (trail == null)
        {
           trail = GetComponent<TrailRenderer>();
        }
        else
        {
            trail.Clear();
        }
        shotgun.ReturnBulletToPool(gameObject);
    }
}

