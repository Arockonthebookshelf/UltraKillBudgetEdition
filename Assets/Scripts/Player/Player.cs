using UnityEngine;

public class Player : MonoBehaviour,IDamagable,IPersistenceData
{
    HUD hud;
    [SerializeField] private int maxHealth = 100;
    int currentHealth;
    [HideInInspector] public bool canHeal = false;

    [SerializeField] private Animator camAnimator;

    private bool isHurt;

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
        isHurt = true;
        if (isHurt)
        {
            HurtAnimation(); //Plays Hurt Animation 
        }
        hud.UpdateHealthBar(currentHealth);
        canHeal = true;
        if(currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            DeathAnimation();
        }
    }
    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount , 0 , maxHealth);
        hud.UpdateHealthBar(currentHealth);
        if(currentHealth < maxHealth)
        {
            canHeal = true;
        }
        else
        {
            canHeal = false;
        }
    }

    public void HurtAnimation()
    {
        camAnimator.Play("Player Hurt", 0, 0f);

        isHurt = false;
    }
    public void DeathAnimation()
    {
        camAnimator.Play("Player Death", 0, 0f);
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
