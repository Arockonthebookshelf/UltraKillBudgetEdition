using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text ammoCount;
    [SerializeField] GameObject interactionProgressBar;
    [SerializeField] GameObject interactionButtonImage;
    [SerializeField] GameObject interactionTextGO;
    Slider interactionSlider;
    Image interactionButton;
    TMP_Text interactionText;
    int mAmmo;
    
     void Awake()
    {
        interactionSlider = interactionProgressBar.GetComponent<Slider>();
        interactionButton = interactionButtonImage.GetComponent<Image>();
        interactionText = interactionTextGO.GetComponent<TMP_Text>();
    }

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
    
    public void UpdateWeapon(string name)
    {
        weaponName.SetText(name + ":");
    }

    public void UpdateAmmo(int currentAmmo)
    {
        ammoCount.SetText(currentAmmo + "/" + mAmmo);
    }

    public void ToggleDisplay(bool value)
    {
        interactionProgressBar.SetActive(value);
        interactionButtonImage.SetActive(value);
        interactionTextGO.SetActive(value);
    }

    public void UpdateInteractionPrompt(bool showButton,float progressbar, string text)
    {
        interactionButton.enabled = showButton;
        interactionSlider.value = progressbar;
        interactionText.SetText(text);
    }
}
