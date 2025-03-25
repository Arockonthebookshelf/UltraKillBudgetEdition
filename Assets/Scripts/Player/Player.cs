using System;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable,IPersistenceData
{
    HUD hud;
    public static Action OnPlayerDeath;
    [SerializeField] private int maxHealth = 100;
    int currentHealth;
    Vector3 checkPointPos;
    [HideInInspector] public bool canHeal = false;

    //[SerializeField] private Animator camAnimator;

    private bool isHurt;

    void Awake()
    {
        hud = FindFirstObjectByType<HUD>();
    }
    void Start()
    {
        hud.InitializeHealthBar(maxHealth);
    }
    void Update()
    {
        hud.UpdateHealthBar(currentHealth);
    }
    public void Damage(float damage,Collider hitCollider)
    {
        if(currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            //DeathAnimation();
            OnPlayerDeath?.Invoke();
            return;
        }
        currentHealth = currentHealth - (int)damage;
        //isHurt = true;
        //if (isHurt)
        //{
        //   // HurtAnimation(); //Plays Hurt Animation 
        //}
        canHeal = true;
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
    void fall()
    {
        transform.position =checkPointPos;
        currentHealth = currentHealth -10;
    }
    //public void HurtAnimation()
    //{
    //    camAnimator.Play("Player Hurt", 0, 0f);

    //    isHurt = false;
    //}
    //public void DeathAnimation()
    //{
    //    camAnimator.Play("Player Death", 0, 0f);
    //}
    public void LoadData(GameData gameData)
    {
        transform.position = gameData.playerPosition;
        checkPointPos = gameData.playerPosition;
        currentHealth = gameData.curHealth;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.playerPosition = transform.position;
        gameData.curHealth = currentHealth;
    }
}
