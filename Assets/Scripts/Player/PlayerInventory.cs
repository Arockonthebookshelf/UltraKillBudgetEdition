using UnityEngine;

public class PlayerInventory : MonoBehaviour , IPersistenceData
{
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

    [HideInInspector]public int ClearanceLevel = 0;
    [HideInInspector]public int currentBulletCount;
    [HideInInspector]public int currentCapacitorCount;
    [HideInInspector]public int currentEnergyCellsCount;
    [HideInInspector]public int currentRocketsCount;
    public bool canPickUpBullets;
    public bool canPickUpCapacitors;
    public bool canPickUpEnergyCells;
    public bool canPickUpRockets;

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

}
