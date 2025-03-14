using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    PlayerInventory playerInventory;

    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text PistolAmmoCount;
    [SerializeField] TMP_Text ShotgunAmmoCount;
    [SerializeField] TMP_Text MinigunAmmoCount;
    [SerializeField] TMP_Text RocketLauncherAmmoCount;
    [SerializeField] GameObject interactionProgressBar;
    [SerializeField] GameObject interactionButtonImage;
    [SerializeField] GameObject interactionTextGO;
    [SerializeField] Color normalAmmoColor;
    [SerializeField] Color lowAmmoColor;
    [SerializeField] Color noAmmoColor;
    [SerializeField] float lowAmmoPercentage = 0.25f;
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
        UpdatePistolAmmo();
        UpdateShotgunAmmo();
        UpdateMiniGunAmmo();
        UpdateRocketLauncherAmmo();
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

    public void UpdatePistolAmmo()
    {
        PistolAmmoCount.SetText(playerInventory.currentBulletCount + "/" + playerInventory.maxBulletCount);
        PistolAmmoCount.color = (playerInventory.currentBulletCount >= (playerInventory.maxBulletCount * lowAmmoPercentage))? normalAmmoColor : (playerInventory.currentBulletCount == 0)? noAmmoColor : lowAmmoColor;
    }

    public void UpdateShotgunAmmo()
    {
        ShotgunAmmoCount.SetText(playerInventory.currentCapacitorCount + "/" + playerInventory.maxCapacitorCount);
        ShotgunAmmoCount.color = (playerInventory.currentCapacitorCount >= playerInventory.maxCapacitorCount * lowAmmoPercentage)? normalAmmoColor : (playerInventory.currentCapacitorCount == 0)? noAmmoColor : lowAmmoColor;
    }
    public void UpdateMiniGunAmmo()
    {
        MinigunAmmoCount.SetText(playerInventory.currentEnergyCellsCount + "/" + playerInventory.maxEnergyCellsCount);
        MinigunAmmoCount.color = (playerInventory.currentEnergyCellsCount >= playerInventory.maxEnergyCellsCount * lowAmmoPercentage)? normalAmmoColor : (playerInventory.currentEnergyCellsCount == 0)? noAmmoColor : lowAmmoColor;
    }
    public void UpdateRocketLauncherAmmo()
    {
        RocketLauncherAmmoCount.SetText(playerInventory.currentRocketsCount + "/" + playerInventory.maxRocketsCount);
        RocketLauncherAmmoCount.color = (playerInventory.currentRocketsCount >= playerInventory.maxRocketsCount * lowAmmoPercentage)? normalAmmoColor : (playerInventory.currentRocketsCount == 0)? noAmmoColor : lowAmmoColor;
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
