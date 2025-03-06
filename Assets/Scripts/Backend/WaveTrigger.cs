using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaveTrigger : MonoBehaviour
{
    [Tooltip("Distance where spawner will be added to this trigger")]
    [SerializeField][Range(0,30)] int spawnerDistance;
    [Tooltip("The total number of wave for the spawner trigger by this trigger")]
    [SerializeField][Range(1,10)] int waveLimit;
    [SerializeField]int waveGrowth;

    private List<WaveSpawner>spawners = new List<WaveSpawner>();
    WaveManager waveManager;
    private List<GameObject> spawnersPosition = new List<GameObject>();

    public static Action<List<WaveSpawner>,int,int> OnSpawnerTriggered;
    #region spawnerSetUp
    void Start()
    {
        GameObject.FindGameObjectsWithTag("Spawner",spawnersPosition);//find Gameobject with Spawner tag.
        waveManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WaveManager>();//find wave manager
       
        List<GameObject> temp = new List<GameObject>();
        foreach (GameObject spawner in spawnersPosition)
        {
            // Debug.Log("all"+spawner.name);
            // //Searching for spawner with the given trigger distance and has a spawner script.Removing those without it.
            if(Mathf.Abs(Vector3.Distance(spawner.transform.position ,transform.position)) < spawnerDistance && spawner.GetComponent<WaveSpawner>())
            {
                temp.Add(spawner);
            // Debug.Log(temp.Count);
            }
            // Debug.Log("spawner only"+spawner.name);
        }
        spawnersPosition = new List<GameObject>(temp);
        //spawnersPosition = temp;
        //Debug.Log(spawnersPosition.Count);
        foreach (GameObject spawnerPosition in temp)
        {
            
           //Debug.Log(spawners.Count);
            spawners.Add(spawnerPosition.GetComponent<WaveSpawner>());
        }
        if (spawners.Count < 1)
        {
            Debug.LogWarning("trigger could not find Spawner. Please increase trigger distance or add the spawner in the trigger radius or add spawner tag to the spawner");
        }
        if (!gameObject.GetComponent<Collider>().isTrigger)
        {
            Debug.LogWarning("Enable isTrigger on colider for spawn trigger to work");
        }
        
        
    }
#endregion
    
    
#region collisionDetection
    private void OnTriggerEnter(Collider other)
    {
            if(other.CompareTag("Player"))
            {
            OnSpawnerTriggered?.Invoke(spawners,waveLimit,waveGrowth);
            }
            gameObject.SetActive(false);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(spawnerDistance,spawnerDistance,spawnerDistance));
    }
    #endregion collisionDetection
}
