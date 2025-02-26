using UnityEngine;

public interface IPersistenceData 
{
     void LoadData(GameData gameData);
     void SaveData(ref GameData gameData);
}
