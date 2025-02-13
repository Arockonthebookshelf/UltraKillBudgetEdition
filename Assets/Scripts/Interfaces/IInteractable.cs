using UnityEngine;
public interface IInteractable
{
    bool Visible();
    bool canInteract(int clearanceLevel);
    float TimeToInteract();
    string InteractionText();
    void CompleteInteraction();
}
