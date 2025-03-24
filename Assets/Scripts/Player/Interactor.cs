    using UnityEngine;

public class Interactor : MonoBehaviour
{
    HUD hud;
    PlayerInventory playerInventory;
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] float interactRange;
    float pressedTime;
    float interactionProgress;

    void Awake()
    {
        hud = FindFirstObjectByType<HUD>();
        hud.ToggleDisplay(false);
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    void Update()
    {
        Ray r = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
        if(Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                if(interactObj.Visible())
                {
                hud.UpdateInteractionPrompt(interactObj.canInteract(playerInventory.ClearanceLevel) , interactionProgress,interactObj.InteractionText());
                hud.ToggleDisplay(true);
                if(Input.GetKey(KeyCode.E) && interactObj.canInteract(playerInventory.ClearanceLevel))
                {
                    pressedTime += Time.deltaTime;
                        if(pressedTime >= interactObj.TimeToInteract())
                            {
                                interactObj.CompleteInteraction();
                                pressedTime = 0;
                                hud.ToggleDisplay(false);
                            }
                        interactionProgress = pressedTime / interactObj.TimeToInteract();
                        Mathf.Clamp(interactionProgress, 0 , 1);
                }
                if(Input.GetKeyUp(KeyCode.E))
                {
                    pressedTime = interactionProgress = 0;
                }
                }
            }
            
        }
        else 
        {
            pressedTime = interactionProgress = 0;
            hud.ToggleDisplay(false);
        }
    }
}
