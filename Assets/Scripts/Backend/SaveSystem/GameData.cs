using UnityEngine;

[System.Serializable]
public class GameData 
{
   public Vector3 playerPosition;
   public Quaternion playerRotation;
   public int curHealth;
   public int ClearanceLevel = 0;
   public int currentBulletCount;
   public int currentshotgunAmmoCount;
   public int currentEnergyCellsCount;
   public int currentRocketsCount;
   public SerializableDictionary<string ,bool> waveActive;
   public bool hasShotgun;
   public bool hasMinigun;
   public bool hasRocketLauncher;

   public GameData()
   {
      playerPosition = new Vector3(0,0,0);
      playerRotation = Quaternion.identity;
      ClearanceLevel = 0;
      currentBulletCount = 25;
      currentshotgunAmmoCount = 15;
      currentEnergyCellsCount = 100;
      currentRocketsCount = 5;
      curHealth = 100;  
      waveActive = new SerializableDictionary<string, bool>();
      hasShotgun = false;
      hasMinigun = false;
      hasRocketLauncher = false;
   }
}
