using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    PlayerInventory playerInventory;

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
    string currentWeapon;
    
     void Awake()
    {
        interactionSlider = interactionProgressBar.GetComponent<Slider>();
        interactionButton = interactionButtonImage.GetComponent<Image>();
        interactionText = interactionTextGO.GetComponent<TMP_Text>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    void Start()
    {
        
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
        currentWeapon = name;
        weaponName.SetText(currentWeapon + ":");
        switch (name)
        {
            case "Pistol":
               UpdateAmmo(playerInventory.currentBulletCount, playerInventory.maxBulletCount);
                break;
            case "Shotgun":
                UpdateAmmo(playerInventory.currentCapacitorCount, playerInventory.maxCapacitorCount);
                break;
            case "MiniGun":
                UpdateAmmo(playerInventory.currentEnergyCellsCount, playerInventory.maxEnergyCellsCount);
                break;
            case "RocketLauncher":
                UpdateAmmo(playerInventory.currentRocketsCount, playerInventory.maxRocketsCount);
                break;
        }
    }

    public void AmmoPickedUp(string name)
    {
       if(name == currentWeapon)
        {
            UpdateWeapon(name);
        }
    }

    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        ammoCount.SetText(currentAmmo + "/" + maxAmmo);
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
