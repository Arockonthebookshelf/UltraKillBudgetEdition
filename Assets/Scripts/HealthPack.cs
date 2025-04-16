using UnityEngine;

public class HealthPack : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = PlayerInventory.instance.GetComponent<Player>();
    }
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
