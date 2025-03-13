using UnityEngine;

public class WaveManager2 : MonoBehaviour
{
[SerializeField] GameObject firstWave;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            firstWave.GetComponent<SingleWave>().SpawnEnemies();
        }
    }
}
