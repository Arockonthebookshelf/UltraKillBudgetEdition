using UnityEngine;

[RequireComponent(typeof(Collider))]

public class SpawnTrigger : MonoBehaviour
{
    [Tooltip("Reference to the spawner")]
    [SerializeField]private SingleSpawner spawner;
    void Start()
    {
      if (!gameObject.GetComponent<Collider>().isTrigger)
        {
            Debug.LogWarning("Enable isTrigger on colider for spawn trigger to work");
        }
        if(spawner == null)
        {
            Debug.LogWarning("Spawner is not assigned");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        spawner.Spawn();
    }
}
