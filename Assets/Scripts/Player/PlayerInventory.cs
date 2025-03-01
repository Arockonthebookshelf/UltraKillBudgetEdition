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
    [SerializeField] private int maxBulletCount = 0;
    [SerializeField] private int maxCapacitorCount = 0;
    [SerializeField] private int maxEnergyCellsCount = 0;
    [SerializeField] private int maxRocketsCount = 0;

    void Update()
    {
        canPickUpBullets = currentBulletCount < maxBulletCount;
        canPickUpCapacitors = currentCapacitorCount < maxCapacitorCount;
        canPickUpEnergyCells = currentEnergyCellsCount < maxEnergyCellsCount;
        canPickUpRockets = currentRocketsCount < maxRocketsCount;
    }

    public void AddBullets(int amount)
    {
        currentBulletCount += amount;
        hud.UpdateAmmo(currentBulletCount);
    }
    public void RemoveBullets(int amount)
    {
        currentBulletCount -= amount;
        hud.UpdateAmmo(currentBulletCount);
    }

}
