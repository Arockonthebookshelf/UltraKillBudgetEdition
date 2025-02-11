using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour , IInteractable
{
    [SerializeField] List<SlidingDoors> doorsToUnlock = new List<SlidingDoors>();
    [SerializeField] List<SlidingDoors> doorsToLock = new List<SlidingDoors>();

    InteractionPrompt interactionPrompt;
    public bool hasKeycard;
    bool visible = true;

    void Awake()
    {
        interactionPrompt = FindFirstObjectByType<InteractionPrompt>();
    }

    public bool Visible()
    {
        return visible;
    }

    public bool canInteract()
    {
        if(hasKeycard)
        return true;
        else
        return false;
    }

    public float TimeToInteract()
    {
        if(hasKeycard)
        return 1;
        else
        return 0;
    }

    public string InteractionText()
    {
        if(hasKeycard)
        return "Scan Keycard";
        else
        return "No keycard";
    }

    public void CompleteInteraction()
    {
        foreach (SlidingDoors doors in doorsToUnlock)
        {
            doors.locked = false;
        }

        foreach (SlidingDoors doors in doorsToLock)
        {
            doors.locked = true;
        }

        visible = false;
    }
}
