using UnityEngine;

public class Player : MonoBehaviour,IDamagable,IPersistenceData
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
    public void LoadData(GameData gameData)
    {
        transform.position = gameData.playerPosition;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.playerPosition = this.transform.position;
    }
}
