using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave 
{   
    public Action OnCurrentEventStop;
    bool enemyIsAlive;
    bool enemyIsSet;
    private List<GameObject> enmeyList = new List<GameObject>();
    public void WaveSpawn(List<Spawner>spawners)
    {
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
        Debug.Log("Update");
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
                 Debug.Log(i);
            }
        }
        if(i > 1)
        {
            enemyIsAlive = true;
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
        if(!enemyIsAlive)
        {
            enemyIsSet = false;
            enmeyList.Clear();
            OnCurrentEventStop?.Invoke();
        }
        //event ???
    }
}
