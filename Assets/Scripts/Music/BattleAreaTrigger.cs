using UnityEngine;

public class BattleAreaTrigger : MonoBehaviour
{
    public MusicManager musicManager;
    public BattleAreaManager battleAreaManager; // assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is on the Player layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            musicManager.PlayBattleMusic();
        }

        // Check if the colliding object is on the Enemy layer
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsEnemy"))
        {
            battleAreaManager.RegisterEnemy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Unregister enemy if it leaves the battle area
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsEnemy"))
        {
            battleAreaManager.UnregisterEnemy(other.gameObject);
        }
    }

}
