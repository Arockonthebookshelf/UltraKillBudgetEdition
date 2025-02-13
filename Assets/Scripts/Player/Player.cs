using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
    [SerializeField] private int playerHealth =100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Damage(float damage,Collider hitCollider)
    {
        playerHealth = playerHealth - (int)damage;
        if(playerHealth < 0)
        {
            Debug.Log("Player is dead");
        }
    }
}
