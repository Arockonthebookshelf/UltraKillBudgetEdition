using UnityEngine;

public class TrailDestroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Delay", 1f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }
}
