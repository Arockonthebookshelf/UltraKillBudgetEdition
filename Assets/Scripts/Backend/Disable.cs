using UnityEngine;

public class Disable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("disable",1);
    }
    void disable()
    {
        gameObject.SetActive(false);
    }
}
