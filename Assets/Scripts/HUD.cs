using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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
    [SerializeField] Image shotgunImage;
    [SerializeField] Image minigunImage;
    [SerializeField] Image rocketLauncherImage;
    [SerializeField] TMP_Text shotgunAmmoCount;
    [SerializeField] TMP_Text minigunAmmoCount;
    [SerializeField] TMP_Text rocketLauncherAmmoCount;
    [SerializeField] GameObject interactionProgressBar;
    [SerializeField] GameObject interactionButtonImage;
    [SerializeField] GameObject interactionTextGO;
    [SerializeField] Color fullAmmoColor;
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
    [SerializeField] Volume damageVolume;
    public GameObject pistolGunUI;
    public GameObject shotGunUI;
    public GameObject miniGunUI;
    public GameObject rocketLauncherUI;
    [SerializeField] RectTransform weaponLayoutGroup;
    RectTransform currentWeaponRT;
    RectTransform pistolRT;
    RectTransform shotgunRT;
    RectTransform minigunRT;
    RectTransform rocketLauncherRT;
    Animator HudAnimator;
    [SerializeField] Image currentCrosshair;
    Slider interactionSlider;
    Image interactionButton;
    TMP_Text interactionText;
    UIBobbing bobbingSway;
    float _maxHealth;

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
        pistolRT = pistolGunUI.GetComponent<RectTransform>();
        shotgunRT = shotGunUI.GetComponent<RectTransform>();
        minigunRT = miniGunUI.GetComponent<RectTransform>();
        rocketLauncherRT = rocketLauncherUI.GetComponent<RectTransform>();
    }

    void Start()
    {
        pistolGunUI.SetActive(PlayerInventory.instance.hasPistol);
        shotGunUI.SetActive(PlayerInventory.instance.hasShotgun);
        miniGunUI.SetActive(PlayerInventory.instance.hasMinigun);
        rocketLauncherUI.SetActive(PlayerInventory.instance.hasRocketLauncher);
        currentWeaponRT = pistolRT;
        currentCrosshair.sprite = pistolCrosshair;
        UpdateShotgunAmmo();
        UpdateMiniGunAmmo();
        UpdateRocketLauncherAmmo();
    }

    public void InitializeHealthBar(int maxHealth)
    {
        healthHitSlider.value = healthHitSlider.maxValue = healthSlider.value = healthSlider.maxValue = maxHealth;
        healthText.SetText(healthSlider.value.ToString());
        _maxHealth = maxHealth;
    }

    public void UpdateHealthBar(int currentHealth)
    {
        healthHitSlider.value = healthSlider.value = currentHealth;
        if (currentHealth > 0)
        {
            healthText.SetText(healthSlider.value.ToString());
            float healthPercentage = currentHealth / _maxHealth;
            damageVolume.weight = 1 - healthPercentage;
        }
        else
        {
            healthText.SetText("Dead");
        }
    }

    public void UpdateShotgunAmmo()
    {
        shotgunAmmoCount.SetText(PlayerInventory.instance.currentshotgunAmmoCount + " / " + PlayerInventory.instance.maxshotgunAmmoCount);
        if(PlayerInventory.instance.currentshotgunAmmoCount == PlayerInventory.instance.maxshotgunAmmoCount)
        {
            shotgunImage.color = shotgunAmmoCount.color = fullAmmoColor;
        }
        else
        {
            shotgunImage.color = shotgunAmmoCount.color = (PlayerInventory.instance.currentshotgunAmmoCount >= PlayerInventory.instance.maxshotgunAmmoCount * lowAmmoPercentage) ? normalAmmoColor : (PlayerInventory.instance.currentshotgunAmmoCount == 0) ? noAmmoColor : lowAmmoColor;
        }
    }
    public void UpdateMiniGunAmmo()
    {
        minigunAmmoCount.SetText(PlayerInventory.instance.currentEnergyCellsCount + " / " + PlayerInventory.instance.maxEnergyCellsCount);
        if (PlayerInventory.instance.currentEnergyCellsCount == PlayerInventory.instance.maxEnergyCellsCount)
        {
            minigunImage.color = minigunAmmoCount.color = fullAmmoColor;
        }
        else
        {
            minigunImage.color = minigunAmmoCount.color = (PlayerInventory.instance.currentEnergyCellsCount >= PlayerInventory.instance.maxEnergyCellsCount * lowAmmoPercentage) ? normalAmmoColor : (PlayerInventory.instance.currentEnergyCellsCount == 0) ? noAmmoColor : lowAmmoColor;
        }    
    }
    public void UpdateRocketLauncherAmmo()
    {
        rocketLauncherAmmoCount.SetText(PlayerInventory.instance.currentRocketsCount + " / " + PlayerInventory.instance.maxRocketsCount);
        if (PlayerInventory.instance.currentRocketsCount == PlayerInventory.instance.maxRocketsCount)
        {
            rocketLauncherImage.color = rocketLauncherAmmoCount.color = fullAmmoColor;
        }
        else
        {
            rocketLauncherImage.color = rocketLauncherAmmoCount.color = (PlayerInventory.instance.currentRocketsCount >= PlayerInventory.instance.maxRocketsCount * lowAmmoPercentage) ? normalAmmoColor : (PlayerInventory.instance.currentRocketsCount == 0) ? noAmmoColor : lowAmmoColor;
        }
    }

    public void UpdateWeapon(string weapon)
    {
        switch (weapon)
        {
            case "Pistol":
                currentCrosshair.sprite = pistolCrosshair;
                currentCrosshair.color = normalCrosshairColor;
                ChangeSelectedWeaponSize(pistolRT);
                break;

            case "Shotgun":
                currentCrosshair.sprite = shotgunCrosshair;
                currentCrosshair.color = normalCrosshairColor;
                ChangeSelectedWeaponSize(shotgunRT);
                break;

            case "MiniGun":
                currentCrosshair.sprite = minigunCrosshair;
                currentCrosshair.color = normalCrosshairColor;
                ChangeSelectedWeaponSize(minigunRT);
                break;

            case "RocketLauncher":
                currentCrosshair.sprite = rocketlauncherCrosshair;
                currentCrosshair.color = normalCrosshairColor;
                ChangeSelectedWeaponSize(rocketLauncherRT);
                break;
        }
    }
    void ChangeSelectedWeaponSize(RectTransform newWeapon)
    {
        currentWeaponRT.sizeDelta = new Vector2(200, 50);
        currentWeaponRT = newWeapon;
        currentWeaponRT.sizeDelta = new Vector2(300, 75);
        LayoutRebuilder.ForceRebuildLayoutImmediate(weaponLayoutGroup);
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
