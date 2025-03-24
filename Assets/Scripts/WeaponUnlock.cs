using System.Diagnostics;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour , IInteractable
{
    PlayerInventory playerInventory;
    HUD hud;
    [SerializeField] string weaponName;
    [SerializeField] float pickupTime;
    [SerializeField] string interactionText;
    void Awake()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        hud = FindFirstObjectByType<HUD>();
    }
    public bool Visible()
    {
        return true;
    }
    public bool canInteract(int clearanceLevel)
    {
        return true;
    }
    public float TimeToInteract()
    {
        return 0.5f;
    }
    public string InteractionText()
    {
        return interactionText;
    }
    public void CompleteInteraction()
    {
        switch(weaponName)
        {
            case "Shotgun":
                playerInventory.hasShotgun = true;
                hud.shotGunUI.SetActive(true);
                break;
            case "Minigun":
                playerInventory.hasMinigun = true;
                hud.miniGunUI.SetActive(true);
                break;
            case "RocketLauncher":
                playerInventory.hasRocketLauncher = true;
                hud.rocketLauncherUI.SetActive(true);
                break;
        }
        Destroy(gameObject);
    }
}
