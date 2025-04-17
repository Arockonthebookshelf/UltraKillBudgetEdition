using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
  public static WaveManager Instance;
  [Tooltip("Use right enemy prefabs ,enemies with subfix 2 are meant to be used")]
    public List<GameObject> spawnEnemies= new List<GameObject>();
    public List<GameObject> EnemyList = new List<GameObject>();
    public Dictionary<WaveSpawner.EnemyType,BaseEnemy2> WaveEnemyDictionary = new Dictionary<WaveSpawner.EnemyType, BaseEnemy2>();
    public MeleeEnemy2 meleeEnemy;
    public ShooterEnemy2 shooterEnemy;
    public TankEnemy2 tankEnemy;
    public Transform tempSpawn;


    void Awake()
    {
        if(Instance != null)
        {
          Destroy(Instance);
        }
        else
        {
          Instance = this;
        }
        WaveEnemyDictionary.Add(WaveSpawner.EnemyType.melee,meleeEnemy);
        WaveEnemyDictionary.Add(WaveSpawner.EnemyType.shooter,shooterEnemy);
        WaveEnemyDictionary.Add(WaveSpawner.EnemyType.tank,tankEnemy);
        if(spawnEnemies.Count < 0)
        {
          Debug.LogWarning("SpawnEnemies cannot be empty");
          return;
        }
        foreach(var enemy in spawnEnemies)
        {
          for(int i =0;i<15;i++)
          {
            GameObject temp = Instantiate(enemy,tempSpawn.position,tempSpawn.rotation);
            EnemyList.Add(temp);
            temp.SetActive(false);
          }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<GameObject> GetEnemiesToList(WaveSpawner.WaveEnemies waveEnemy)
    {
        List<GameObject>temp;
        temp = EnemyList;
        return temp;
    }
    public void RemoveEnemyFromPool(List<GameObject> currentWave)
    {
      List<GameObject>temp = new List<GameObject>();
      foreach(var enemy in currentWave)
      {
        temp.Add(enemy);
      }
      currentWave.Clear();
      foreach(var enemy in temp)
      {
        EnemyList.Add(enemy);
      }
    }
    public void ReturnToList(List<GameObject> objectsToBeAdded)
    {
      foreach(var enemy in objectsToBeAdded)
      {
        EnemyList.Add(enemy);
      }
      objectsToBeAdded.Clear();
    }
  
}
