using UnityEngine;

public class WaveManager2 : MonoBehaviour
{
    [SerializeField] GameObject firstWave;

    private Collider triggerCollider;


    private void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            firstWave.GetComponent<SingleWave>().SpawnEnemies();

            triggerCollider.enabled = false;
        }
    }
}
