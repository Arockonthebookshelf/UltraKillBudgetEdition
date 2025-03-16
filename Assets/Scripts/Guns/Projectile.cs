using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f; 
    [SerializeField] float damage;

    private Shotgun shotgun;
    HitIndicator hitIndicator;
    void Awake()
    {
        shotgun = FindFirstObjectByType<Shotgun>();
        hitIndicator = FindFirstObjectByType<HitIndicator>();
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
            hitIndicator.Hit();
        }

        DisableProjectile();
    }

    private void DisableProjectile()
    {
        shotgun.ReturnBulletToPool(gameObject);
    }
    
}

