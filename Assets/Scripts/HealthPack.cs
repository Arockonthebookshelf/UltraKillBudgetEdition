using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField]Player player;
    [SerializeField] int healAmount = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.currentHealth < player.maxHealth)
        {
            other.GetComponent<Player>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
