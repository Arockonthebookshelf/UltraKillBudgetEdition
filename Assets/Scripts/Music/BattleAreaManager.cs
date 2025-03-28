using System.Collections.Generic;
using UnityEngine;

public class BattleAreaManager : MonoBehaviour
{
    public MusicManager musicManager;
    public List<GameObject> enemies;

    // You might want to call this whenever an enemy is spawned or added
    public void RegisterEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    // Call this from your enemy's death logic
    public void UnregisterEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        // Remove any destroyed (null) enemy references
        enemies.RemoveAll(item => item == null);

        // When the cleaned-up list is empty, stop the battle music
        if (enemies.Count == 0)
        {
            musicManager.StopBattleMusic();
        }
    }
}
