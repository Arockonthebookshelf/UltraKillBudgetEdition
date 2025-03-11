using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave 
{   
    public Action OnCurrentEventStop;
    public static Action OnwaveStart;
    public static Action OnwaveActive;
    public static Action OnwaveStop;
    bool enemyIsAlive;
    bool enemyIsSet;
    private List<GameObject> enmeyList = new List<GameObject>();
    public void WaveSpawn(List<WaveSpawner>spawners)
    {
        OnwaveStart?.Invoke();
        foreach(var spawner in spawners)
        {
            spawner.Spawn();
            foreach (GameObject obj in spawner.objects)
            {
                enmeyList.Add(obj);
                enemyIsSet = true;
            }
        }
    }
    public void WaveUpdate()
    {
        //list of enemies and check if they are active
        if(!enemyIsSet)
        {
            return;
        }
        int i = enmeyList.Count;
        foreach(GameObject obj in enmeyList)
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
        if(!enemyIsAlive || enmeyList.Count == 0)
        {
            OnwaveStop?.Invoke();
            enemyIsSet = false;
            enmeyList.Clear();
            OnCurrentEventStop?.Invoke();
        }
        //event ???
    }
}
