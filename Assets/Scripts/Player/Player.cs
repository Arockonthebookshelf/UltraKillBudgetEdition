using UnityEngine;

public class Player : MonoBehaviour,IDamagable,IPersistenceData
{
    HUD hud;
    [SerializeField] private int maxHealth = 100;
    int currentHealth;

    void Awake()
    {
        hud = FindFirstObjectByType<HUD>();
    }
    void Start()
    {
        hud.InitializeHealthBar(maxHealth);
        currentHealth = maxHealth;
    }
    public void Damage(float damage,Collider hitCollider)
    {
        currentHealth = currentHealth - (int)damage;
        hud.UpdateHealthBar(currentHealth);
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
        gameData.playerPosition = transform.position;
    }
}
