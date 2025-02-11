using UnityEngine;
public interface IInteractable
{
    bool Visible();
    bool canInteract();
    float TimeToInteract();
    string InteractionText();
    void CompleteInteraction();
}
