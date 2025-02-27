    using UnityEngine;

public class Interactor : MonoBehaviour
{
    InteractionPrompt interactionPrompt;
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] float interactRange;
    [SerializeField] int clearanceLevel;
    float pressedTime;
    float interactionProgress;

    void Awake()
    {
        interactionPrompt = FindFirstObjectByType<InteractionPrompt>();
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
                interactionPrompt.UpdateInteractionPrompt(interactObj.canInteract(clearanceLevel) , interactionProgress,interactObj.InteractionText());
                interactionPrompt.ToggleDisplay(true);
                if(Input.GetKey(KeyCode.E) && interactObj.canInteract(clearanceLevel))
                {
                    pressedTime += Time.deltaTime;
                        if(pressedTime >= interactObj.TimeToInteract())
                            {
                                interactObj.CompleteInteraction();
                                interactionPrompt.ToggleDisplay(false);
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
                    interactionPrompt.ToggleDisplay(false);
                    interactionPrompt.ResetInteractionPrompt();
        }
    }
}
