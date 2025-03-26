using UnityEngine;

[System.Serializable]
public class GameData 
{
   public Vector3 playerPosition;
   public int curHealth;
   public int ClearanceLevel = 0;
   public int currentBulletCount;
   public int currentCapacitorCount;
   public int currentEnergyCellsCount;
   public int currentRocketsCount;
   public SerializableDictionary<string ,bool> waveActive;

   public GameData()
   {
      playerPosition = new Vector3(0,0,0);
      ClearanceLevel = 0;
      currentBulletCount = 25;
      currentCapacitorCount = 15;
      currentEnergyCellsCount = 100;
      currentRocketsCount = 5;
      curHealth = 100;  
      waveActive = new SerializableDictionary<string, bool>();
   }
}
