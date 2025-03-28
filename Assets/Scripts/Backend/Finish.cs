using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Action OnLevelFinished;
    [SerializeField]private GameObject GameOver;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Finish");
            GameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            OnLevelFinished?.Invoke();
        }
    }
}
