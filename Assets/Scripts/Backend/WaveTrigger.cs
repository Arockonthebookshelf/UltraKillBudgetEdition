using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaveTrigger : MonoBehaviour
{
    private Spawner[]spawners;
    [SerializeField][Range(0,30)] int triggerDIstance;
    WaveManager waveManager;
    private GameObject[] spawnersPosition;
    void Start()
    {
        spawnersPosition =GameObject.FindGameObjectsWithTag("Spawner");//find Gameobject with Spawner tag.
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();//find wave manager
       
        int i = 0;
        GameObject[] temp = new GameObject[spawnersPosition.Length];
        foreach (GameObject spawner in spawnersPosition)
        {
            //Debug.Log("all"+spawner.name);
            //Searching for spawner with the given trigger distance and has a spawner script.Removing those without it.
            if(Mathf.Abs(Vector3.Distance(spawner.transform.position ,transform.position)) < triggerDIstance && spawner.GetComponent<Spawner>() != null)
            {
                temp[i] = spawner;
                //Debug.Log("temp"+temp[i]);
                i++;
            }
        }
        spawnersPosition = temp;
        spawners = new Spawner[spawnersPosition.Length];
        i = 0;
        foreach (GameObject spawnerPosition in spawnersPosition)
        {
            //Debug.Log("Change" + spawnerPosition);
            spawners[i] = spawnerPosition.GetComponent<Spawner>();
            //Debug.Log("Change" + spawners[i]);
            i++;
        }
        if (spawnersPosition.Length < 1)
        {
            Debug.LogWarning("trigger could not find Spawner. Please increase trigger distance or add the spawner in the trigger radius");
        }
        if (!gameObject.GetComponent<Collider>().isTrigger)
        {
            Debug.LogWarning("Enable isTrigger on colider for spawn trigger to work");
        }
        
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colides");
            if(other.CompareTag("Player"))
            {
            //    Debug.Log("Player");
            //    foreach(Spawner spawner in spawners)
            //{
            //    spawner.Spawn();
            //}
            waveManager.GetSpawner(spawners);
            }
    }
}
