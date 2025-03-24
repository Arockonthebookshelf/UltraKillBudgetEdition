using UnityEngine;

public class PlayerInventory : MonoBehaviour , IPersistenceData
{
    HUD hud;
    void Awake()
    {
        hud = FindFirstObjectByType<HUD>();
    }

    public void LoadData(GameData gameData)
    {
        ClearanceLevel = gameData.ClearanceLevel;
        currentBulletCount = gameData.currentBulletCount;
        currentCapacitorCount = gameData.currentCapacitorCount;
        currentEnergyCellsCount = gameData.currentEnergyCellsCount;
        currentRocketsCount = gameData.currentRocketsCount;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.ClearanceLevel = ClearanceLevel;
        gameData.currentBulletCount = currentBulletCount;
        gameData.currentCapacitorCount = currentCapacitorCount;
        gameData.currentEnergyCellsCount = currentEnergyCellsCount;
        gameData.currentRocketsCount = currentRocketsCount;
    }

    public int ClearanceLevel = 0;
    public int currentBulletCount;
    public int currentCapacitorCount;
    public int currentEnergyCellsCount;
    public int currentRocketsCount;
    [HideInInspector]public bool canPickUpBullets;
    [HideInInspector]public bool canPickUpCapacitors;
    [HideInInspector]public bool canPickUpEnergyCells;
    [HideInInspector]public bool canPickUpRockets;

    [Header("Ammo Settings")]
    public int maxBulletCount = 0;
    public int maxCapacitorCount = 0;
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
        canPickUpCapacitors = currentCapacitorCount < maxCapacitorCount && hasShotgun;
        canPickUpEnergyCells = currentEnergyCellsCount < maxEnergyCellsCount && hasMinigun;
        canPickUpRockets = currentRocketsCount < maxRocketsCount && hasRocketLauncher;
    }

    public void AddBullets(int amount)
    {
        if(currentBulletCount + amount > maxBulletCount)
        {
            currentBulletCount = maxBulletCount;
        }
        else
        {
            currentBulletCount += amount;
        }
        hud.UpdatePistolAmmo();
    }
    public void RemoveBullets(int amount)
    {
        currentBulletCount -= amount;
        hud.UpdatePistolAmmo();
    }

    public void AddCapacitors(int amount)
    {
        if(currentCapacitorCount + amount > maxCapacitorCount)
        {
            currentCapacitorCount = maxCapacitorCount;
        }
        else
        {
            currentCapacitorCount += amount;
        }
        hud.UpdateShotgunAmmo();
    }
    public void RemoveCapacitors(int amount)
    {
        currentCapacitorCount -= amount;
        hud.UpdateShotgunAmmo();
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
        hud.UpdateMiniGunAmmo();
    }
    public void RemoveEnergyCells(int amount)
    {
        currentEnergyCellsCount -= amount;
        hud.UpdateMiniGunAmmo();
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
        hud.UpdateRocketLauncherAmmo();
    }
    public void RemoveRockets(int amount)
    {
        currentRocketsCount -= amount;
        hud.UpdateRocketLauncherAmmo();
    }

    public void CanShoot(bool value)
    {
        hud.UpdateCrosshairColor(value);
    }
}
