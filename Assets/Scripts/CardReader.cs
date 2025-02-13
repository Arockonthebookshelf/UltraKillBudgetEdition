using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour , IInteractable
{
    [SerializeField] List<SlidingDoors> doorsToUnlock = new List<SlidingDoors>();
    [SerializeField] List<SlidingDoors> doorsToLock = new List<SlidingDoors>();
    [SerializeField] int clearanceLevelRequired;
    
    bool hasClearance;
    bool visible = true;

    public bool Visible()
    {
        return visible;
    }

    public bool canInteract(int clearanceLevel)
    {
        if(clearanceLevel >= clearanceLevelRequired)
        {
            hasClearance = true;
        }

        if(hasClearance)
        return true;
        else
        return false;
    }

    public float TimeToInteract()
    {
        if(hasClearance)
        return 1;
        else
        return 0;
    }

    public string InteractionText()
    {
        if(hasClearance)
        return "Scan Keycard";
        else
        return "Level " + clearanceLevelRequired.ToString() + " Clearance Required" ;
    }

    public void CompleteInteraction()
    {
        foreach (SlidingDoors doors in doorsToUnlock)
        {
            doors.UnlockDoor();
        }

        foreach (SlidingDoors doors in doorsToLock)
        {
            doors.LockDoor();
        }

        visible = false;
    }
}
