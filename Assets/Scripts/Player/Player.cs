using UnityEngine;

public class Player : MonoBehaviour,IDamagable,IPersistenceData
{
    HealthBar healthBar;
    [SerializeField] private int maxHealth = 100;
    int currentHealth;

    void Awake()
    {
        healthBar = FindFirstObjectByType<HealthBar>();
    }
    void Start()
    {
        healthBar.InitializeHealthBar(maxHealth);
        currentHealth = maxHealth;
    }
    public void Damage(float damage,Collider hitCollider)
    {
        currentHealth = currentHealth - (int)damage;
        healthBar.UpdateHealthBar(currentHealth);
        if(currentHealth <= 0)
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
