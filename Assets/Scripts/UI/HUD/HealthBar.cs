using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text healthText;

    public void InitializeHealthBar(int maxHealth)
    {
        healthSlider.value = healthSlider.maxValue = maxHealth;
        healthText.SetText(healthSlider.value.ToString());
    }

    public void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;
        healthText.SetText(healthSlider.value.ToString());
    }
}

