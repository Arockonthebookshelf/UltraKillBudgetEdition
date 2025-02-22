using UnityEngine;

public class persistentSaveManager : MonoBehaviour
{
    public static persistentSaveManager instance{get;private set;}
    GameData gameData;
    void Awake()
    {
         if(instance != null)
        {
                Debug.Log("Muliple instance of singleton save manager");
                Destroy(instance);
        }
        instance = this;
    }

    public void NewGame()
    {
        //intialize new game data
        GameData gameData = new GameData();
    }
    public void LoadGame()
    {
        if(this.gameData == null)
        {
            NewGame();
        }
    }
    public void SaveGame()
    {

    }
    public void OnSaveAndQuit()
    {

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
