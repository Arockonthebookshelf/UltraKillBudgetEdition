using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;
    [SerializeField] float damage;
    [SerializeField] private string bulletTag = "Shotgun Projectile";

    HitIndicator hitIndicator;
    void Awake()
    {
        hitIndicator = FindFirstObjectByType<HitIndicator>();
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
        gameObject.SetActive(false);
        ObjectPooler.Instance.poolDictionary[bulletTag].Enqueue(gameObject);
    }
}

