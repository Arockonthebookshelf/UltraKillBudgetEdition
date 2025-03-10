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

    void Update()
    {
        canPickUpBullets = currentBulletCount < maxBulletCount;
        canPickUpCapacitors = currentCapacitorCount < maxCapacitorCount;
        canPickUpEnergyCells = currentEnergyCellsCount < maxEnergyCellsCount;
        canPickUpRockets = currentRocketsCount < maxRocketsCount;
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
        hud.AmmoPickedUp("Pistol");
    }
    public void RemoveBullets(int amount)
    {
        currentBulletCount -= amount;
        hud.AmmoPickedUp("Pistol");
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
        hud.AmmoPickedUp("Shotgun");
    }
    public void RemoveCapacitors(int amount)
    {
        currentCapacitorCount -= amount;
        hud.AmmoPickedUp("Shotgun");
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
        hud.AmmoPickedUp("MiniGun");
    }
    public void RemoveEnergyCells(int amount)
    {
        currentEnergyCellsCount -= amount;
        hud.AmmoPickedUp("MiniGun");
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
        hud.AmmoPickedUp("RocketLauncher");
    }
    public void RemoveRockets(int amount)
    {
        currentRocketsCount -= amount;
        hud.AmmoPickedUp("RocketLauncher");
    }

}
