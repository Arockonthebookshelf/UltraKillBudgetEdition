using UnityEngine;

public class KeycardPickup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.instance.IncreaseClearanceLevel();
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + rotationSpeed, transform.localEulerAngles.z);
    }
}
