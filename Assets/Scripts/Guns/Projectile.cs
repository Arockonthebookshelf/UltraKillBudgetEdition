using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;
    [SerializeField] float damage;
    [SerializeField] private string bulletTag = "Shotgun Projectile";
    [SerializeField] GameObject bloodPrefab;

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
            if (collision.collider.CompareTag("Enemy"))
            {
                Vector3 hitPoint = collision.GetContact(0).point;
                Vector3 hitNormal = collision.GetContact(0).normal;
                Instantiate(bloodPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            }
            else
            {
                // hit game object or particle effect
            }
        }

        DisableProjectile();
    }

    private void DisableProjectile()
    {
        gameObject.SetActive(false);
        ObjectPooler.Instance.poolDictionary[bulletTag].Enqueue(gameObject);
    }
}

