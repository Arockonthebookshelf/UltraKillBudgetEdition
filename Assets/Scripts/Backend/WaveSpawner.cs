using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]int waveLimit;
    public enum EnemyType
    {
        melee,shooter,tank
    }
   
    [System.Serializable]
    public class WaveEnemy
    {
        public EnemyType waveEnemy = new EnemyType();
        public Transform spawner;
    }
    [System.Serializable]
    public class WaveEnemies
    {
        public WaveEnemy[] waveEnemies = new WaveEnemy[15];
    }
    public WaveEnemies[] Enemies = new WaveEnemies[10];
    public List<GameObject>currentWave = new List<GameObject>();
    public List<GameObject>enemyInPool;
    public WaveManager waveManager;
    public bool wavesIsActive = false;
    public Wave wave;
    private WaveEnemies currentWaveEnemy;
    private int currentWaveIndex=0;
    public SlidingDoors[] doors;
    void Awake()
    {
        if(gameObject.tag != "Spawner")
        {
            gameObject.tag = "Spawner";
        }
         foreach(var enemy in Enemies)
        {
            waveLimit++;
        }
        currentWaveEnemy = Enemies[0];
    }
    void Start()
    {
        currentWaveEnemy = Enemies[0];
       AddEnemyToCurrentWave(currentWaveEnemy);
    }

    // Update is called once per frame
    void Update()
    {
       enemyInPool = waveManager.GetEnemiesToList(currentWaveEnemy);
       if(wavesIsActive)
       {
            wave.WaveUpdate();
       }
    }
    public void AddEnemyToCurrentWave(WaveEnemies waveEnemies)
    {
        List<EnemyType> enemiesType = new List<EnemyType>();
        foreach(var enemy in waveEnemies.waveEnemies)
        {
        
            enemiesType.Add(enemy.waveEnemy);
        }
        BaseEnemy2[] enemiesBase= new BaseEnemy2[15];
        int i=0;
        foreach(var enemy in enemiesType)
        {
            if(i>14)
            {
                break;
            }
            waveManager.WaveEnemyDictionary.TryGetValue(enemy,out enemiesBase[i]);
            //Debug.Log(enemiesBase[i]);
            i++;
        }
        foreach(var enemy in enemiesBase)
        {
            if(enemy)
            {
                foreach(var InPool in enemyInPool)
                {
                    Debug.Log(  InPool.GetComponent<BaseEnemy2>().GetType()); 
                    if(enemy.GetType() == InPool.GetComponent<BaseEnemy2>().GetType() && !InPool.activeInHierarchy)
                    {
                        Debug.Log("time");
                        currentWave.Add(InPool);
                        enemyInPool.Remove(InPool);
                        break;
                    }
                }
            }
        }
        List<Transform>enemiesPosition= new List<Transform>();
        foreach(var enemyPosition in waveEnemies.waveEnemies)
        {
            
            enemiesPosition.Add(enemyPosition.spawner);
        }
        i=0;
        foreach(var enemy in currentWave)
        {
            if(enemiesPosition[i] == null)
            {
                i++;
                continue;
            }
            enemy.transform.position = enemiesPosition[i].position;
            i++;
        }
    }
    public void OnwaveStart()
    {
        Debug.Log("waveStart");
        foreach(var door in doors)
        {
            door.LockDoor();
        }
        enemyInPool = waveManager.GetEnemiesToList(currentWaveEnemy);
        wavesIsActive = true;
        wave.WaveStart(ref currentWave,this);
    }
    
  
    public void NextWave()
    {
        currentWaveIndex++;
        if(currentWaveIndex >= waveLimit)
        {
            wavesIsActive = false;
            if(doors.Length >0)
            {
                foreach(var door in doors)
                {
                    door.UnlockDoor();
                }
                }
            return;
        }
        Debug.Log(currentWaveIndex);
        
        currentWaveEnemy = Enemies[currentWaveIndex];
        waveManager.RemoveEnemyFromPool(currentWave);
        AddEnemyToCurrentWave(currentWaveEnemy);
        wave.WaveStart(ref currentWave,this);
    }
   IEnumerator WaitTime()
   {
        bool waitForEnemy=true;
        while(waitForEnemy)
        {
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
   }
}
