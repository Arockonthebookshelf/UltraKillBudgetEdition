using System;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField]GameObject spawnObject;
    GameObject enemyContainer;
    [Tooltip("Total Enemy that can be spawned")]
    [SerializeField] [Range(0,15)]int batchLimit;
    [Tooltip("Enemy number spawn by a spawner. Enemy number increase with wave")]
    [SerializeField] [Range(1,5)] int currentSpawnLimit;
    WaveManager waveManager;
    public bool spawnAt;
    [SerializeField] int spawnAtWave;
    [SerializeField]private Transform patrolPointA;
    [SerializeField]private Transform patrolPointB;
    public List <GameObject>objects = new List<GameObject>();
    private void Awake()
    {
        if(gameObject.tag != "Spawner")
        {
            gameObject.tag = "Spawner";
        }
    }
    private void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WaveManager>();

        if (spawnObject == null)
        {
            Debug.LogError("Object to be spawned cannot be empty");
            return;
        }
        for (int i = 0; i < batchLimit; i++)
        {
            Vector3 randomPos = new Vector3(random.Range(-2, 2), 0, random.Range(-2, 2));
            GameObject obj = Instantiate(spawnObject, transform.position + randomPos, Quaternion.identity, transform);
            objects.Add(obj);
        }
        foreach (GameObject obj in objects)
        {
            if(obj.GetComponent<BaseEnemy>())
            {
                obj.GetComponent<BaseEnemy>().patrolPointA = patrolPointA;
                obj.GetComponent<BaseEnemy>().patrolPointB = patrolPointB;
            }   
            obj.SetActive(false);
        }
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
        foreach (GameObject obj in objects)
        {
            if(spawnCounter >= currentSpawnLimit)
            {
                break;
            }
            Vector3 randomPos = new Vector3(random.Range(-2, 2), 0, random.Range(-2, 2));
            obj.transform.position = transform.position + randomPos;
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
        }
    }

}
