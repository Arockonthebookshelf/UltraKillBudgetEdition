using UnityEngine;

public class LootBox : MonoBehaviour , IDamagable
{
    [SerializeField]  GameObject itemDropperPrefab;
    float health = 1f;
    bool isLooted = false;

    public void Damage(float damage, Collider collider)
    {
        health = health - damage;
        if(health <= 0 && !isLooted)
        {
            isLooted = true;
            Instantiate(itemDropperPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

}
