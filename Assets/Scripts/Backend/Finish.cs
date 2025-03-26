using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Action OnLevelFinished;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Finish");
            OnLevelFinished?.Invoke();
        }
    }
}
