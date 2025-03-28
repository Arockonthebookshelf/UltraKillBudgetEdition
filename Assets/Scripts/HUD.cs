using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance = null;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider healthHitSlider;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text healText;
    [SerializeField] float healTextTimer;
    [SerializeField] TMP_Text clearanceLevelText;
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
    [SerializeField] Sprite pistolCrosshair;
    [SerializeField] Sprite shotgunCrosshair;
    [SerializeField] Sprite minigunCrosshair;
    [SerializeField] Sprite rocketlauncherCrosshair;
    [SerializeField] Color normalCrosshairColor;
    [SerializeField] Color disabledCrosshairColor;
    [SerializeField] Animator selectedWeapon;
    public GameObject shotGunUI;
    public GameObject miniGunUI;
    public GameObject rocketLauncherUI;
    Animator HudAnimator;
    [SerializeField] Image currentCrosshair;
    Slider interactionSlider;
    Image interactionButton;
    TMP_Text interactionText;
    UIBobbing bobbingSway;
    void Awake()
    {
        if(instance!=this)
        {
            Destroy(instance);
        }
        if (!instance)
        {
            instance = this;
        }

        interactionSlider = interactionProgressBar.GetComponent<Slider>();
        interactionButton = interactionButtonImage.GetComponent<Image>();
        interactionText = interactionTextGO.GetComponent<TMP_Text>();
        
        HudAnimator = GetComponent<Animator>();
        bobbingSway = FindAnyObjectByType<UIBobbing>();
    }

    void Start()
    {
        currentCrosshair.sprite = pistolCrosshair;
        UpdateShotgunAmmo();
        UpdateMiniGunAmmo();
        UpdateRocketLauncherAmmo();
    }

    public void InitializeHealthBar(int maxHealth)
    {
        healthHitSlider.value = healthHitSlider.maxValue = healthSlider.value = healthSlider.maxValue = maxHealth;
        healthText.SetText(healthSlider.value.ToString());
    }

    public void UpdateHealthBar(int currentHealth)
    {
        healthHitSlider.value = healthSlider.value = currentHealth;
        if (currentHealth > 0)
        {
            healthText.SetText(healthSlider.value.ToString());
        }
        else
        {
            healthText.SetText("Dead");
        }
    }

    public void UpdateShotgunAmmo()
    {
        ShotgunAmmoCount.SetText(PlayerInventory.instance.currentshotgunAmmoCount + " / " + PlayerInventory.instance.maxshotgunAmmoCount);
        ShotgunAmmoCount.color = (PlayerInventory.instance.currentshotgunAmmoCount >= PlayerInventory.instance.maxshotgunAmmoCount * lowAmmoPercentage) ? normalAmmoColor : (PlayerInventory.instance.currentshotgunAmmoCount == 0) ? noAmmoColor : lowAmmoColor;
    }
    public void UpdateMiniGunAmmo()
    {
        MinigunAmmoCount.SetText(PlayerInventory.instance.currentEnergyCellsCount + " / " + PlayerInventory.instance.maxEnergyCellsCount);
        MinigunAmmoCount.color = (PlayerInventory.instance.currentEnergyCellsCount >= PlayerInventory.instance.maxEnergyCellsCount * lowAmmoPercentage) ? normalAmmoColor : (PlayerInventory.instance.currentEnergyCellsCount == 0) ? noAmmoColor : lowAmmoColor;
    }
    public void UpdateRocketLauncherAmmo()
    {
        RocketLauncherAmmoCount.SetText(PlayerInventory.instance.currentRocketsCount + " / " + PlayerInventory.instance.maxRocketsCount);
        RocketLauncherAmmoCount.color = (PlayerInventory.instance.currentRocketsCount >= PlayerInventory.instance.maxRocketsCount * lowAmmoPercentage) ? normalAmmoColor : (PlayerInventory.instance.currentRocketsCount == 0) ? noAmmoColor : lowAmmoColor;
    }

    public void UpdateWeapon(string weapon)
    {
        switch (weapon)
        {
            case "Pistol":
                currentCrosshair.sprite = pistolCrosshair;
                currentCrosshair.color = normalCrosshairColor;
                selectedWeapon.SetInteger("Weapon", 0);
                break;

            case "Shotgun":
                currentCrosshair.sprite = shotgunCrosshair;
                currentCrosshair.color = normalCrosshairColor;
                selectedWeapon.SetInteger("Weapon", 1);
                break;

            case "MiniGun":
                currentCrosshair.sprite = minigunCrosshair;
                currentCrosshair.color = normalCrosshairColor;
                selectedWeapon.SetInteger("Weapon", 2);
                break;

            case "RocketLauncher":
                currentCrosshair.sprite = rocketlauncherCrosshair;
                currentCrosshair.color = normalCrosshairColor;
                selectedWeapon.SetInteger("Weapon", 3);
                break;
        }
    }

    public void ToggleDisplay(bool value)
    {
        interactionProgressBar.SetActive(value);
        interactionButtonImage.SetActive(value);
        interactionTextGO.SetActive(value);
    }

    public void UpdateInteractionPrompt(bool showButton, float progressbar, string text)
    {
        interactionButton.enabled = showButton;
        interactionSlider.value = progressbar;
        interactionText.SetText(text);
    }

    public void UpdateCrosshairColor(bool value)
    {
        if (value)
        {
            currentCrosshair.color = normalCrosshairColor;
        }
        else
        {
            currentCrosshair.color = disabledCrosshairColor;
        }
    }

    public void DamageEffect()
    {
        HudAnimator.SetTrigger("Damaged");
        bobbingSway.TriggerHitEffect();
    }

    public void ShowHealthChangeText(int healAmount)
    {
        healText.SetText("+"+healAmount.ToString());
        Invoke("HideHealthChangeText", healTextTimer);
    }

    void HideHealthChangeText()
    {
        healText.SetText("");
    }

    public void UpdateClearanceLevel()
    {
        clearanceLevelText.SetText(PlayerInventory.instance.ClearanceLevel.ToString());
    }
}
