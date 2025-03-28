using UnityEngine;

public class LootBox : MonoBehaviour , IDamagable
{
    [SerializeField]  GameObject itemDropperPrefab;
    float health = 10f;
    
    public void Damage(float damage, Collider collider)
    {
        health = health - damage;
        if(health < 0)
        {
            Instantiate(itemDropperPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

}
