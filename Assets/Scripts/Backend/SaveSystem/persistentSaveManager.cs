using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class persistentSaveManager : MonoBehaviour
{
    public static persistentSaveManager instance{get;private set;}
    [SerializeField] private string FileName;
    [SerializeField]private Transform playerStartPoint;
    private DataHandler datahandler;
    GameData gameData;
    private List<IPersistenceData> persistenceDataObjects;
    public static Action newGame;
    void Awake()
    {
         if(instance != null)
        {
                Debug.Log("Muliple instance of singleton save manager");
                Destroy(instance);
        }
        instance = this;
    }
    void OnEnable()
    {
        CheckPoint.OnTriggered += SaveGame;
        Finish.OnLevelFinished += NewGame;
        Player.OnPlayerReloaded += LoadGame;
        PauseMenu.OnRestart += NewGame;
    }
    void OnDisable()
    {
        CheckPoint.OnTriggered -= SaveGame;
        Finish.OnLevelFinished -= NewGame;
        Player.OnPlayerReloaded -= LoadGame;
        PauseMenu.OnRestart -= NewGame;
    }
    public void Start()
    {
        this.datahandler = new DataHandler(Application.persistentDataPath, FileName);
        this.persistenceDataObjects = FindAllPersistenceDataObjects();
        LoadGame();
    }
    public void NewGame()
    {
        //intialize new game data
        gameData = new GameData();
        gameData.playerPosition = playerStartPoint.position;
    }
    public void Restart()
    {
        gameData = null;
    }
    public void LoadGame()
    {
        gameData = datahandler.LoadData();
        if(gameData == null)
        {
            NewGame();
        }
        foreach(IPersistenceData persistenceData in persistenceDataObjects)
        {
            persistenceData.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        foreach(IPersistenceData persistenceData in persistenceDataObjects)
        {
            persistenceData.SaveData(ref gameData);
        }
        datahandler.SaveData(gameData);
        LoadGame();
    }
    
    private List<IPersistenceData> FindAllPersistenceDataObjects()
    {
        //IEnumerable<IPersistenceData> persistenceDatasObjects = FindObjectsOfType<MonoBehaviour>().OfType<IPersistenceData>();
        IEnumerable<IPersistenceData> persistenceDatasObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPersistenceData>();
        return new List<IPersistenceData> (persistenceDatasObjects);
    }
}
