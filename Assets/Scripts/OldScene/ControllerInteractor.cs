using UnityEngine;

[RequireComponent(typeof(ControllerEvents))]
public class ControllerInteractor : MonoBehaviour
{
    private ControllerEvents controllerEvents;

    private InteractableWithController currentInteraction;
    private bool holdingCurrentInteraction = false;

    private bool leftTriggerOfHeldInteraction = false; // True if we leave the trigger but are still grabbing the object

    void Awake()
    {
        controllerEvents = GetComponent<ControllerEvents>();
    }

    void OnEnable()
    {
        controllerEvents.TriggerPressed += HandleTriggerPressed;
        controllerEvents.TriggerReleased += HandleTriggerReleased;
    }

    void OnDisable()
    {
        controllerEvents.TriggerPressed -= HandleTriggerPressed;
        controllerEvents.TriggerReleased -= HandleTriggerReleased;
    }

    void OnTriggerEnter(Collider other)
    {
        InteractableWithController interactable = other.GetComponent<InteractableWithController>();

        // If we don't have a current interaction
        if(!currentInteraction && interactable)
        {
            interactable.OnControllerEnter();
            currentInteraction = interactable;
        }
        else if(holdingCurrentInteraction && interactable && currentInteraction == interactable)
        {
            leftTriggerOfHeldInteraction = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If we have a current interaction and we are not holding it
        if(currentInteraction && !holdingCurrentInteraction)
        {
            currentInteraction.OnControllerExit();
            currentInteraction = null;
        }
        // We are still holding the current interaction, but the trigger left, don't call the OnControllerExit method
        else if(currentInteraction && holdingCurrentInteraction)
        {
            leftTriggerOfHeldInteraction = true;
        }
    }

    private void HandleTriggerPressed(object sender, ControllerEvents.ControllerInteractionEventArgs e)
    {
        // If we are hovering the current interaction, holdingCurrentInteraction will be true if the interactable object wants to be grabbed
        if (currentInteraction)
        {
            currentInteraction.OnInteractableGrabbed(this.transform, e, out holdingCurrentInteraction);                                             
        }
    }

    private void HandleTriggerReleased(object sender, ControllerEvents.ControllerInteractionEventArgs e)
    {
        // If we are holding an interaction
        if(holdingCurrentInteraction)
        {
            holdingCurrentInteraction = false;

            // Order matters...
            if(leftTriggerOfHeldInteraction)
            {
                currentInteraction.OnControllerExit();
            }

            currentInteraction.OnInteractableReleased(this.transform, e);

            // ...Which is why this gets broken up
            if (leftTriggerOfHeldInteraction)
            {
                currentInteraction = null;
                leftTriggerOfHeldInteraction = false;
            }
        }
    }
}
