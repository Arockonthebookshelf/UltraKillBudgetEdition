using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Tooltip("Gap between waves")]
    public int waveCountDown;
    private Wave wave = new Wave();
    public GameObject enemyContainer;
    public GameObject meleeContainer;
    public GameObject rangeContainer;
    public GameObject bossContainer;
    public GameObject tankContainer;

    [ReadOnly(true)]
    public List<GameObject> spawnEnemies = new List<GameObject>();
    public List<GameObject> meleeEnemies = new List<GameObject>();
    public List<GameObject> shooterEnemies = new List<GameObject>();
    public List<GameObject> tankEnemies = new List<GameObject>();
    public List<GameObject> bossEnemies = new List<GameObject>();
    private List<WaveSpawner> _spawners = new List<WaveSpawner>();
    public int currentWave;
    private int waveMax;
    private int maxBatchsize;
    bool waitForWave;
    private int _waveGrowth;
    private bool waveIsActive;


    private void OnEnable()
    {
        WaveTrigger.OnSpawnerTriggered += WaveStart;
        wave.OnCurrentEventStop += WavesUpdate;
    }
    void Awake()
    {
        enemyContainer = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        enemyContainer.name = "EnemyContainer"; 
        meleeContainer = Instantiate(new GameObject(), enemyContainer.transform);
        meleeContainer.name = "MeleeContainer"; 
        rangeContainer = Instantiate(new GameObject(), enemyContainer.transform);
        rangeContainer.name = "RangeContainer";
        bossContainer = Instantiate(new GameObject(), enemyContainer.transform);
        bossContainer.name = "BossContainer";
        tankContainer = Instantiate(new GameObject(), enemyContainer.transform);    
        tankContainer.name = "TankContainer";
        
    }
    void Start()
    {
        List<GameObject> Spawner = new List<GameObject>();
        GameObject.FindGameObjectsWithTag("Spawner",Spawner);
        int curBatchSize = 0;
        foreach(GameObject obj in Spawner)
        {
            curBatchSize = obj.GetComponent<WaveSpawner>().batchLimit;
          if(maxBatchsize < curBatchSize)
          {
            maxBatchsize = curBatchSize;
          }   
        }
        foreach(GameObject enemy in spawnEnemies)
        {
            for(int i = 0; i < maxBatchsize; i++)
            {
            GameObject container;
            if(enemy.GetComponent<MeleeEnemy>())
            {
                container = meleeContainer;
            }
            else if(enemy.GetComponent<ShooterEnemy>())
            {
                container = rangeContainer;
            }
            else if(enemy.GetComponent<TankEnemy>())
            {
                container = tankContainer;
            }
            else if(enemy.GetComponent<BossEnemy>())
            {
                container = bossContainer;
            }
            else
            {
                container = enemyContainer;
            }
                GameObject temp = Instantiate(enemy, container.transform);
                if(enemy.GetComponent<MeleeEnemy>())
            {
                meleeEnemies.Add(temp);
            }
            else if(enemy.GetComponent<ShooterEnemy>())
            {
                shooterEnemies.Add(temp);
            }
            else if(enemy.GetComponent<TankEnemy>())
            {
                tankEnemies.Add(temp);
            }
            else if(enemy.GetComponent<BossEnemy>())
            {
                bossEnemies.Add(temp);
            }
                temp.SetActive(false);
            }
        }
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(waveIsActive )
        {
            wave.WaveUpdate();
        }
    }

    void WaveStart(List<WaveSpawner> spawners,int waveLimit,int waveGrowth)
    {
        if(spawners.Capacity == 0)
        {
            return;
        }
        foreach(WaveSpawner spawner in spawners)
        {
            spawner.SpawnerReset();
        }
        _spawners = spawners;
        waveMax = waveLimit;
        _waveGrowth = waveGrowth;
        currentWave = 0;
        wave.WaveSpawn(spawners);
        waveIsActive = true;

    }
    void WavesUpdate()
    {
        if(currentWave < waveMax)
        {
            foreach(WaveSpawner spawner in _spawners)
            {
                spawner.IncreaseBatchSize(_waveGrowth);
            }
            currentWave++;
            waitForWave = true;
            StartCoroutine("SpawnAfterWaitTime");
            // wave.WaveSpawn(_spawners);
            // waveIsActive = true;
        }
        else
        {
            Debug.Log("Wave over");
            foreach(WaveSpawner spawner in _spawners)
            {
                Debug.Log("Check");
                List <GameObject> enemyList = new List<GameObject>();
                //Destroy(spawner.gameObject);
                if(spawner.SpawnObject.GetComponent<MeleeEnemy>())
                {
                    spawner.ReturnEnemyToList(ref meleeEnemies);
                }
                else if(spawner.SpawnObject.GetComponent<ShooterEnemy>())
                {
                     spawner.ReturnEnemyToList(ref shooterEnemies);
                }
                else if(spawner.SpawnObject.GetComponent<TankEnemy>())
                {
                     spawner.ReturnEnemyToList(ref tankEnemies);
                }
                else if(spawner.SpawnObject.GetComponent<BossEnemy>())
                {
                     spawner.ReturnEnemyToList(ref bossEnemies);
                }
                spawner.gameObject.SetActive(false);

            }

            waveIsActive = false;
        }
    }
    
    public void SetToList(GameObject enemy)
    {
        //List<GameObject> temp ;
         if(enemy.GetComponent<MeleeEnemy>())
            {
                meleeEnemies.Add(enemy);
                //meleeEnemies.Add(enemy);
            }
            else if(enemy.GetComponent<ShooterEnemy>())
            {
                shooterEnemies.Add(enemy);
                //shooterEnemies.Add(enemy);
            }
            else if(enemy.GetComponent<TankEnemy>())
            {
                tankEnemies.Add(enemy);
                //tankEnemies.Add(enemy);
            }
            else if(enemy.GetComponent<BossEnemy>())
            {
                bossEnemies.Add(enemy);
                //bossEnemies.Add(enemy);
            }
            else
            {
                return;
            }
            
            //temp.Add(enemy);
            
    }
    public List<GameObject> GetList(GameObject enemy)
    {
        List<GameObject> enemyList;
         if(enemy.GetComponent<MeleeEnemy>())
            {
               enemyList = meleeEnemies;
            }
            else if(enemy.GetComponent<ShooterEnemy>())
            {
                enemyList = shooterEnemies;
            }
            else if(enemy.GetComponent<TankEnemy>())
            {
                enemyList = tankEnemies;
                Debug.Log("Tank");
            }
            else if(enemy.GetComponent<BossEnemy>())
            {
                enemyList = bossEnemies;
            }
            else{
                return null;
            }
            return enemyList;
            
    }
    public void RemoveEnemy(GameObject enemy)
    {
        if(enemy.GetComponent<MeleeEnemy>())
            {
               meleeEnemies.Remove(enemy);
            }
            else if(enemy.GetComponent<ShooterEnemy>())
            {
                shooterEnemies.Remove(enemy);;
            }
            else if(enemy.GetComponent<TankEnemy>())
            {
                tankEnemies.Remove(enemy);;
                Debug.Log("Tank");
            }
            else if(enemy.GetComponent<BossEnemy>())
            {
                bossEnemies.Remove(enemy);;
            }
    }
    

    private void OnDisable()
    {
        StopAllCoroutines();
        WaveTrigger.OnSpawnerTriggered -= WaveStart;
        wave.OnCurrentEventStop -= WavesUpdate;
    }
    IEnumerator SpawnAfterWaitTime()
    {
        while(waitForWave)
        {
            yield return new WaitForSeconds(waveCountDown);
            wave.WaveSpawn(_spawners);
            waveIsActive = true;
            waitForWave = false;
        }
    }
}
