using UnityEngine;

public interface IEnemy
{   
    /// <summary>
    /// Enemy Patrols on the the Path
    /// </summary>
    void Patrol();

    /// <summary>
    ///  Enemy will chase the player if it come under the Chasing Range
    /// </summary>
    void Chase();

    /// <summary>
    /// Enemy will chase the player if it come under the Attacking Range
    /// </summary>
    /// </summary>
    void Attack();
}
