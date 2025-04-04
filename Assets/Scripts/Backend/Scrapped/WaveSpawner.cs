// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using random = UnityEngine.Random;


// // public class WaveSpawnerScrapped  : MonoBehaviour
// {
//     GameObject enemyContainer;
//     [Range(0,15)]public int batchLimit;
//     [Tooltip("Enemy number spawn by a spawner. Enemy number increase with wave")]
//     [Range(1,5)]public int intialSpawnLimit;
//      int currentSpawnLimit;
//     WaveManager waveManager;
//     public bool spawnAt;
//     [SerializeField] int spawnAtWave;
//     [SerializeField]private Transform patrolPointA;
//     [SerializeField]private Transform patrolPointB;
//     [SerializeField]private List <SpawnerPosition> spawnPoints = new List<SpawnerPosition>();
//     [System.Serializable]
//     public class WaveEnemy
//     {
//         public GameObject[] waveEnemy = new GameObject[15];
//     }    
//     public List <WaveEnemy>objects = new List<WaveEnemy>();
//     public List <GameObject>enemies = new List<GameObject>();
//     private void Awake()
//     {
//         if(gameObject.tag != "Spawner")
//         {
//             gameObject.tag = "Spawner";
//         }
//     }
//     private void Start()
//     {
//         #region dependency
//         waveManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WaveManager>();
//         enemyContainer = waveManager.enemyContainer;

//             //enemies = waveManager.GetList(spawnObject);
//             if(spawnPoints.Count < 1)
//             {
                
//             }
//         #endregion
    
//     }
   
//     public void Spawn()
//     {
//         if(spawnAt)
//         {
//             if(waveManager.currentWave < spawnAtWave)
//             {
//                 return;
//             }
//         }
//         int spawnCounter = 0;
//         int index = 0;
//         foreach (var obj in objects)
//         {
//             foreach(var enemy in obj.waveEnemy)
//             {
//                 if(spawnCounter >= currentSpawnLimit)
//                 {
//                     break;
//                 }
//                 if(index > spawnPoints.Count-1)
//                 {
//                     index = 0;
//                 }
//                 Vector3 randomPos = new Vector3(random.Range(-2, 2), 0, random.Range(-2, 2));
//                 enemy.transform.position = spawnPoints[index].Transform.position;
//                 index++;
//                 if(enemy.GetComponent<BaseEnemy>())
//                 {
//                     enemy.GetComponent<BaseEnemy>().Reset();
//                 }
//                 enemy.SetActive(true);
//                 spawnCounter++;
//             }
//         }

//     }
    
//     public List<GameObject> GetEnemies(int batchLimit)
//     {
//         List<GameObject> temp = new List<GameObject>();
//         int spawnedEnemies =0;
//         foreach(GameObject enemy in enemies)
//         {
//             if(!enemy.activeInHierarchy && batchLimit >= temp.Count)
//             {
//                 temp.Add(enemy);
//                 spawnedEnemies++;
//             } 
//          }

//          foreach (var enemy in temp)
//          {
//             waveManager.RemoveEnemy(enemy);
//          }
//         temp = InstiateNewEnemies(temp);
        
//         return temp;
//     }
//     private List<GameObject> InstiateNewEnemies(List <GameObject> currentSpawnerEnemyList)
//     {
//         int counter = 0;
//         foreach(var enemy in currentSpawnerEnemyList)
//         {
//             counter++;
//         }
//         int remaining = batchLimit - counter;
//         for(int i=0;i<remaining;i++)
//         {
    
    
//         }
//             return currentSpawnerEnemyList;
//     }
//     public void ReturnEnemyToList(ref List<GameObject> enemyList)
//     {
//         // foreach(var enemy in objects[0])
//         // {
//         //     enemyList.Add(enemy);
//         // }
//         // objects.Clear();
//     }
//     public void SpawnerReset()
//     {
//         currentSpawnLimit = intialSpawnLimit;
//     }


// }
