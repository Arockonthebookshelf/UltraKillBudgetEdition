using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] int healAmount = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
