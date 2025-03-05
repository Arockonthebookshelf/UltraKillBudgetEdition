using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    [Tooltip("Time in seconds before this GameObject gets destroyed.")]
    public float destroyTime = 5f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
