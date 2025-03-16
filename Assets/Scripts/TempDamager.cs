using UnityEngine;

public class TempDamager : MonoBehaviour , IDamagable
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        collision.gameObject.GetComponent<Player>().Damage(10,collision.collider);
    }

    public void Damage(float damage, Collider collider)
    {
        
    }
}
