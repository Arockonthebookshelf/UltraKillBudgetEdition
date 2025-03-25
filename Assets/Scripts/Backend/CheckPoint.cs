using System;

using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Action OnTriggered;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnTriggered?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
