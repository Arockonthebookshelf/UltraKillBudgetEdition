using UnityEngine;

public class PlayerInventory : MonoBehaviour , IPersistenceData
{
    
    public static PlayerInventory instance = null;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        
    }

    public void LoadData(GameData gameData)
    {
        ClearanceLevel = gameData.ClearanceLevel;
        currentBulletCount = gameData.currentBulletCount;
        currentshotgunAmmoCount = gameData.currentshotgunAmmoCount;
        currentEnergyCellsCount = gameData.currentEnergyCellsCount;
        currentRocketsCount = gameData.currentRocketsCount;
        hasMinigun = gameData.hasMinigun;
        hasShotgun = gameData.hasShotgun;
        hasRocketLauncher = gameData.hasRocketLauncher;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.ClearanceLevel = ClearanceLevel;
        gameData.currentBulletCount = currentBulletCount;
        gameData.currentshotgunAmmoCount = currentshotgunAmmoCount;
        gameData.currentEnergyCellsCount = currentEnergyCellsCount;
        gameData.currentRocketsCount = currentRocketsCount;
        gameData.hasShotgun = hasShotgun;
        gameData.hasMinigun = hasMinigun;
        gameData.hasRocketLauncher = hasRocketLauncher;
    }

    public int ClearanceLevel = 0;
    public int currentBulletCount;
    public int currentshotgunAmmoCount;
    public int currentEnergyCellsCount;
    public int currentRocketsCount;
    [HideInInspector]public bool canPickUpBullets;
    [HideInInspector]public bool canPickUpShotgunAmmo;
    [HideInInspector]public bool canPickUpEnergyCells;
    [HideInInspector]public bool canPickUpRockets;

    [Header("Ammo Settings")]
    public int maxBulletCount = 0;
    public int maxshotgunAmmoCount = 0;
    public int maxEnergyCellsCount = 0;
    public int maxRocketsCount = 0;

    [Header("Weapons Settings")]
    public bool hasPistol = true;
    public bool hasShotgun = false;
    public bool hasMinigun = false;
    public bool hasRocketLauncher = false;


    void Update()
    {
        canPickUpBullets = currentBulletCount < maxBulletCount && hasPistol;
        canPickUpShotgunAmmo = currentshotgunAmmoCount < maxshotgunAmmoCount && hasShotgun;
        canPickUpEnergyCells = currentEnergyCellsCount < maxEnergyCellsCount && hasMinigun;
        canPickUpRockets = currentRocketsCount < maxRocketsCount && hasRocketLauncher;
    }

    public void AddShotgunAmmo(int amount)
    {
        if(currentshotgunAmmoCount + amount > maxshotgunAmmoCount)
        {
            currentshotgunAmmoCount = maxshotgunAmmoCount;
        }
        else
        {
            currentshotgunAmmoCount += amount;
        }
        HUD.instance.UpdateShotgunAmmo();
    }
    public void RemoveShotgunAmmo(int amount)
    {
        currentshotgunAmmoCount -= amount;
        HUD.instance.UpdateShotgunAmmo();
    }

    public void AddEnergyCells(int amount)
    {
        if(currentEnergyCellsCount + amount > maxEnergyCellsCount)
        {
            currentEnergyCellsCount = maxEnergyCellsCount;
        }
        else
        {
            currentEnergyCellsCount += amount;
        }
        HUD.instance.UpdateMiniGunAmmo();
    }
    public void RemoveEnergyCells(int amount)
    {
        currentEnergyCellsCount -= amount;
        HUD.instance.UpdateMiniGunAmmo();
    }

    public void AddRockets(int amount)
    {
        if(currentRocketsCount + amount > maxRocketsCount)
        {
            currentRocketsCount = maxRocketsCount;
        }
        else
        {
            currentRocketsCount += amount;
        }
        HUD.instance.UpdateRocketLauncherAmmo();
    }
    public void RemoveRockets(int amount)
    {
        currentRocketsCount -= amount;
        HUD.instance.UpdateRocketLauncherAmmo();
    }

    public void CanShoot(bool value)
    {
        HUD.instance.UpdateCrosshairColor(value);
    }

    public void IncreaseClearanceLevel()
    {
        ClearanceLevel++;
        HUD.instance.UpdateClearanceLevel();
    }
}
