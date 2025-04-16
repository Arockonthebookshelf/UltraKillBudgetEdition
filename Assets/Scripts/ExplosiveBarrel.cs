using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject explosion;
    [SerializeField] float barrelHealth;
    [SerializeField] int explosionDamage;
    [SerializeField] float explosionRange;
    [SerializeField] LayerMask layer;

    public AudioSource barrelSound;

    public void Damage(float damage, Collider collider)
    {
        barrelHealth -= damage;
        if (barrelHealth <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        barrelSound.Play();
        Instantiate(explosion, transform.position, Quaternion.identity);
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, layer);
        for (int i = 0; i < enemies.Length; i++)
        {
            IDamagable damagable = enemies[i].GetComponent<IDamagable>();
            damagable.Damage(explosionDamage, enemies[i]);
        }
        Destroy(gameObject);
    }
}
