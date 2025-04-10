using UnityEngine;

public class Plasma : MonoBehaviour
{
    [SerializeField] int diableTimer;
    [SerializeField] float Damage;

    void OnEnable()
    {
        Invoke(nameof(DisableObj), diableTimer);
    }
    void DisableObj()
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        ObjectPooler.Instance.EnqueObject(transform.parent.name, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(Damage, other);
                DisableObj();
            }
        }
    }
}
