using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;
    [SerializeField] float damage;
    [SerializeField] private string bulletTag = "Shotgun Projectile";
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] GameObject hitPrefab;

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
        Vector3 hitPoint = collision.GetContact(0).point;
        Vector3 hitNormal = collision.GetContact(0).normal;
        if (damageable != null)
        {
            damageable.Damage(damage, collision.collider);
            hitIndicator.Hit();
            
            if (collision.collider.CompareTag("Enemy"))
            {       
                Instantiate(bloodPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            }    
        }
        else
        {
            Instantiate(hitPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
        }

        DisableProjectile();
    }

    private void DisableProjectile()
    {
        gameObject.SetActive(false);
        ObjectPooler.Instance.poolDictionary[bulletTag].Enqueue(gameObject);
    }
}

