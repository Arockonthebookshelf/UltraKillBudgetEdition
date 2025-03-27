    using UnityEngine;

public class Interactor : MonoBehaviour
{
    
    
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] float interactRange;
    float pressedTime;
    float interactionProgress;

    void Start()
    {
        HUD.instance.ToggleDisplay(false);
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
                HUD.instance.UpdateInteractionPrompt(interactObj.canInteract(PlayerInventory.instance.ClearanceLevel) , interactionProgress,interactObj.InteractionText());
                HUD.instance.ToggleDisplay(true);
                if(Input.GetKey(KeyCode.E) && interactObj.canInteract(PlayerInventory.instance.ClearanceLevel))
                {
                    pressedTime += Time.deltaTime;
                        if(pressedTime >= interactObj.TimeToInteract())
                            {
                                interactObj.CompleteInteraction();
                                pressedTime = 0;
                                HUD.instance.ToggleDisplay(false);
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
            HUD.instance.ToggleDisplay(false);
        }
    }
}
