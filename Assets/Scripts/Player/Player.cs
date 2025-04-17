using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour,IDamagable,IPersistenceData
{
    
    PlayerMovement movement;
    public static Action OnPlayerDeath;
    public static Action OnPlayerReloaded;
    public int maxHealth = 100;
    [SerializeField]Vector3 fallHeight;
    public int currentHealth;
    Vector3 checkPointPos;
    [HideInInspector] public bool canHeal = false;

    public AudioSource hurtSound;

    private bool isHurt;

    void Awake()
    {
        
    }
    void OnEnable()
    {
        OnPlayerDeath += death;
    }
    void OnDisable()
    {
        OnPlayerDeath -= death;
    }
    void Start()
    {
        HUD.instance.InitializeHealthBar(maxHealth);
        HUD.instance.UpdateHealthBar(currentHealth);
        if(fallHeight == null)
        {
            fallHeight = new Vector3(0,100,0);
        }
        transform.position = checkPointPos;
    }

    public void Damage(float damage,Collider hitCollider)
    {
        hurtSound.Play();
        currentHealth = currentHealth - (int)damage;
        canHeal = true;
        HUD.instance.UpdateHealthBar(currentHealth);
        HUD.instance.DamageEffect();
        if (currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
            return;
        }
    }
    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount , 0 , maxHealth);
        HUD.instance.UpdateHealthBar(currentHealth);
        HUD.instance.ShowHealthChangeText(healAmount);
        if (currentHealth < maxHealth)
        {
            canHeal = true;
        }
        else
        {
            canHeal = false;
        }
    }
    private void death()
    {
       PauseMenu.instance.Dead();
		OnPlayerReloaded?.Invoke();

    }

    public void LoadData(GameData gameData)
    {
       
        transform.position = gameData.playerPosition;
        checkPointPos = gameData.playerPosition;
        currentHealth = gameData.curHealth;
        if(movement!=null)
        movement.enabled = true;
    }
    public void SaveData(ref GameData gameData)
    {
        Debug.Log("saving");
        gameData.playerPosition = transform.position;  
        gameData.curHealth = currentHealth;
    }
    
    void OnTriggerEnter(Collider other)
    {
            if(other.CompareTag("Fall"))
            {
                Damage(10,other);
                transform.position = checkPointPos;
                gameObject.TryGetComponent<PlayerMovement>(out movement);
                movement.rb.linearVelocity = Vector3.zero;
            }
    }
}
