using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public WaveSpawner spawner;
   
     void OnEnable()
    {
        persistentSaveManager.newGame += ActivateOnRestart;
    }
    void OnDisable()
    {
        persistentSaveManager.newGame -= ActivateOnRestart;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            return;
        }
        Debug.Log("Trigger");
        if(spawner == null)
        {
            return;
        }
            spawner.OnwaveStart();
            gameObject.SetActive(false);
    }
     void ActivateOnRestart()
    {
        gameObject.SetActive(true);
    }
}
