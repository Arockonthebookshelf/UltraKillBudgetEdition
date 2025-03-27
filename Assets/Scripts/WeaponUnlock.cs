using System.Diagnostics;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour , IInteractable
{
    
    
    [SerializeField] string weaponName;
    [SerializeField] float pickupTime;
    [SerializeField] string interactionText;
    void Awake()
    {
        
        
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
                PlayerInventory.instance.hasShotgun = true;
                HUD.instance.shotGunUI.SetActive(true);
                break;
            case "Minigun":
                PlayerInventory.instance.hasMinigun = true;
                HUD.instance.miniGunUI.SetActive(true);
                break;
            case "RocketLauncher":
                PlayerInventory.instance.hasRocketLauncher = true;
                HUD.instance.rocketLauncherUI.SetActive(true);
                break;
        }
        Destroy(gameObject);
    }
}
