using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    public void InitializeHealthBar(int maxHealth)
    {
        healthSlider.value = healthSlider.maxValue = maxHealth;
    }

    public void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}

