using System;
using UnityEngine;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{
   WaveSpawner spawner;
   public Action OnCurrentEventStop;
    public static Action OnwaveStart;
    public static Action OnwaveActive;
    public static Action OnwaveStop;
    bool enemyIsAlive;
    bool enemyIsSet;
    public List<GameObject> enemyList;

    public void WaveStart(ref List<GameObject> currentWave,WaveSpawner _spawner)
    {
        spawner = _spawner;
        enemyIsAlive = true;
        enemyList = currentWave;
        OnwaveStart?.Invoke();
        foreach(var enemy in spawner.currentWave)
        {
            enemy.SetActive(true);
        }
        if(currentWave.Count >0)
        {
            enemyIsSet = true;
        }
    }
        public void WaveUpdate()
    {
        //list of enemies and check if they are active
        // foreach(var enemy in enemyList)
        // {
        //     Debug.Log(enemy);
        // }
        if(!enemyIsSet)
        {
            return;
        }
        if(!enemyIsAlive)
        {
            waveStop();
        }
        int i = enemyList.Count;
        foreach(GameObject obj in spawner.currentWave)
        {
            if(!obj.activeInHierarchy)
            {
                i--;
            }
        }
        if(i >= 1)
        {
            enemyIsAlive = true;
            OnwaveActive?.Invoke();
        }
        else
        {
            waveStop();
            enemyIsAlive = false;
        }
        //cur wave only
    }
    public void waveStop()
    {
        //stop check wave and check if next wave exist or not.
        if(!enemyIsAlive || enemyList.Count == 0)
        {
            OnwaveStop?.Invoke();
            enemyIsSet = false;
            spawner.NextWave();
            if(spawner.wavesIsActive)
            {
                spawner.OnwaveStart();
            }
            else{
                Debug.Log("wave over");
                spawner.ReturnEnemies(enemyList);
            }
            OnCurrentEventStop?.Invoke();
        }

    }
   
}
