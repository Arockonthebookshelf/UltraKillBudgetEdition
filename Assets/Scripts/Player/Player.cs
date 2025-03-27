using System;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable,IPersistenceData
{
    HUD hud;
    PlayerMovement movement;
    public static Action OnPlayerDeath;
    public static Action OnPlayerReloaded;
    [SerializeField] private int maxHealth = 100;
    [SerializeField]Vector3 fallHeight;
    [SerializeField] int currentHealth;
    Vector3 checkPointPos;
    Rigidbody rb;
    [HideInInspector] public bool canHeal = false;

    //[SerializeField] private Animator camAnimator;

    private bool isHurt;

    void Awake()
    {
        hud = FindFirstObjectByType<HUD>();
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
        if(fallHeight == null)
        {
            fallHeight = new Vector3(0,100,0);
        }
        hud.InitializeHealthBar(maxHealth);
    }

    public void Damage(float damage,Collider hitCollider)
    {
        hud.UpdateHealthBar(currentHealth);
        hud.DamageEffect();
        if (currentHealth <= 0)
        {
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
    private void death()
    {
        //play Animation
        gameObject.TryGetComponent<PlayerMovement>(out movement);
        movement.enabled = false;
        movement.rb.linearVelocity  = Vector3.zero;
        OnPlayerReloaded?.Invoke();
        //enable gameover UI
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
        Debug.Log("loading");
        transform.position = gameData.playerPosition;
        checkPointPos = gameData.playerPosition;
        transform.rotation = gameData.playerRotation;
        currentHealth = gameData.curHealth;
        if(movement!=null)
        movement.enabled = true;
    }
    public void SaveData(ref GameData gameData)
    {
        Debug.Log("saving");
        gameData.playerPosition = transform.position;
        gameData.playerRotation = transform.rotation;
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
