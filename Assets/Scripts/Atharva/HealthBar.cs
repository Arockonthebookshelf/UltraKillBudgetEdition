using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public int MaxHealth = 80;
    int DDamage;

    void Start()
    {
        MaxHealthValue(MaxHealth);
        DDamage = MaxHealth;
    }

    public void MaxHealthValue(int health)
    {
        Slider.maxValue = health;
        Slider.value = health;
    }
    public void UpdateHealth(int health)
    {
        Slider.value = health;
    }
    void TakeDamage(int Damage)
    {
        DDamage -= Damage;
        UpdateHealth(DDamage);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Example: Press space to take 20 damage
        {
            TakeDamage(20);
        }
    }
}


//   public int MaxHealth = 100;
//public int Currenthealth = 100;
