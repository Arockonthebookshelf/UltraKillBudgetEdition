using System;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField]GameObject spawnObject;
    public GameObject SpawnObject{get{return spawnObject;}}
    GameObject enemyContainer;
    [Tooltip("Total Enemy that can be spawned")]
    [Range(0,15)]public int batchLimit;
    [Tooltip("Enemy number spawn by a spawner. Enemy number increase with wave")]
    [Range(1,5)]public int intialSpawnLimit;
     int currentSpawnLimit;
    WaveManager waveManager;
    public bool spawnAt;
    [SerializeField] int spawnAtWave;
    [SerializeField]private Transform patrolPointA;
    [SerializeField]private Transform patrolPointB;
    [SerializeField]private List <Transform> spawnPoints = new List<Transform>();
    public List <GameObject>objects = new List<GameObject>();
    public List <GameObject>enemies = new List<GameObject>();
    private void Awake()
    {
        if(gameObject.tag != "Spawner")
        {
            gameObject.tag = "Spawner";
        }
    }
    private void Start()
    {
        #region dependency
        waveManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WaveManager>();
        enemyContainer = waveManager.enemyContainer;
        // if(spawnObject.GetComponent<MeleeEnemy>())
        //     {
        //        enemies = waveManager.meleeEnemies;
        //     }
        //     else if(spawnObject.GetComponent<ShooterEnemy>())
        //     {
        //         enemies = waveManager.shooterEnemies;
        //     }
        //     else if(spawnObject.GetComponent<TankEnemy>())
        //     {
        //         enemies = waveManager.tankEnemies;
        //     }
        //     else if(spawnObject.GetComponent<BossEnemy>())
        //     {
        //         enemies = waveManager.bossEnemies;
        //     }
            enemies = waveManager.GetList(spawnObject);
            if(spawnPoints.Count < 1)
            {
                spawnPoints.Add(gameObject.transform);
            }
        #endregion

        if (spawnObject == null)
        {
            Debug.LogError("Object to be spawned cannot be empty");
            return;
        }
        #region instantiate objects
        // for (int i = 0; i < batchLimit; i++)
        // {
        //     Vector3 randomPos = new Vector3(random.Range(-2, 2), 0, random.Range(-2, 2));
        //     GameObject obj = Instantiate(spawnObject, transform.position + randomPos, Quaternion.identity, enemyContainer.transform);
        //     waveManager.enemies.Add(obj);
        // }
        objects = GetEnemies(currentSpawnLimit);
       
        foreach (GameObject obj in objects)
        {
            if(obj.GetComponent<BaseEnemy>())
            {
                obj.GetComponent<BaseEnemy>().patrolPointA = patrolPointA;
                obj.GetComponent<BaseEnemy>().patrolPointB = patrolPointB;
            }   
        }
            #endregion
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
   
    public void Spawn()
    {
        if (spawnObject == null)
        {
            return;
        }
        if(spawnAt)
        {
            if(waveManager.currentWave < spawnAtWave)
            {
                return;
            }
        }
        int spawnCounter = 0;
        int index = 0;
        foreach (GameObject obj in objects)
        {
            if(spawnCounter >= currentSpawnLimit)
            {
                break;
            }
            if(index > spawnPoints.Count-1)
            {
                index = 0;
            }
            Vector3 randomPos = new Vector3(random.Range(-2, 2), 0, random.Range(-2, 2));
            obj.transform.position = spawnPoints[index].position;
            index++;
            if(obj.GetComponent<BaseEnemy>())
            {
                obj.GetComponent<BaseEnemy>().Reset();
            }
            obj.SetActive(true);
            spawnCounter++;
        }

    }
    public void IncreaseBatchSize(int spawnIncrement)
    {
        if (currentSpawnLimit < batchLimit)
        {
            currentSpawnLimit += spawnIncrement;
            objects = GetEnemies(batchLimit);
        }
    }
    public List<GameObject> GetEnemies(int batchLimit)
    {
        List<GameObject> temp = new List<GameObject>();
        int spawnedEnemies =0;
        foreach(GameObject enemy in enemies)
        {
            if(!enemy.activeInHierarchy && batchLimit >= temp.Count)
            {
                temp.Add(enemy);
                spawnedEnemies++;
            } 
         }

         foreach (var enemy in temp)
         {
            waveManager.RemoveEnemy(enemy);
         }
        //  if(batchLimit <= temp.Count)
        //  {
        //     Debug.Log("return gate");
        //     return temp;
        //  }
         int remaining =  batchLimit - spawnedEnemies;
        temp = InstiateNewEnemies(temp);
        //  for(int i=0;i <remaining;i++)
        //  {
        //     GameObject obj = Instantiate(spawnObject,enemyContainer.transform);
        //     waveManager.SetList(obj);
        //     enemies = waveManager.GetList(spawnObject);
        //     temp.Add(obj);
        //     obj.SetActive(false);
        //  }

        //foreach to check vaiable enemies
        // add enemies remaining
        
        return temp;
    }
    private List<GameObject> InstiateNewEnemies(List <GameObject> currentSpawnerEnemyList)
    {
        int counter = 0;
        foreach(var enemy in currentSpawnerEnemyList)
        {
            counter++;
        }
        int remaining = batchLimit - counter;
        for(int i=0;i<remaining;i++)
        {
            GameObject obj = Instantiate(spawnObject,enemyContainer.transform);
            currentSpawnerEnemyList.Add(obj);
        }
            return currentSpawnerEnemyList;
    }
    public void ReturnEnemyToList(ref List<GameObject> enemyList)
    {
        foreach(var enemy in objects)
        {
            enemyList.Add(enemy);
        }
        objects.Clear();
    }
    public void SpawnerReset()
    {
        currentSpawnLimit = intialSpawnLimit;
    }


}
