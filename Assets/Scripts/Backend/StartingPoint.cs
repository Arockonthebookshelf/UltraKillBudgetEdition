using System;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    GameObject player;
    public bool shotGun;
    public bool miniGun;
    public bool rocketLauncher;
    public PlayerInventory inventory;
    void OnEnable()
    {
        PauseMenu.OnRestart += OnRestart;
    }
    void OnDisable()
    {
        PauseMenu.OnRestart -= OnRestart;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
        inventory.hasShotgun = shotGun;
        inventory.hasMinigun = miniGun;
        inventory.hasRocketLauncher = rocketLauncher;
    }
    void OnRestart()
    {
        inventory.hasShotgun = shotGun;
        inventory.hasMinigun = miniGun;
        inventory.hasRocketLauncher = rocketLauncher;
    }

}
