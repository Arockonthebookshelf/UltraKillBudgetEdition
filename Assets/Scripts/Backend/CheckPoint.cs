using System;

using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnEnable()
    {
        persistentSaveManager.newGame += ActivateOnRestart;
    }
    void OnDisable()
    {
        persistentSaveManager.newGame -= ActivateOnRestart;
    }
    public static Action OnTriggered;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnTriggered?.Invoke();
            gameObject.SetActive(false);
        }
    }
    void ActivateOnRestart()
    {
        gameObject.SetActive(true);
    }
}
