using UnityEngine;

public class Disable : MonoBehaviour
{
    public int diableTimer;
    // Update is called once per frame
    void Update()
    {
        Invoke("disable", diableTimer);
    }
    void disable()
    {
        gameObject.SetActive(false);
    }
}
