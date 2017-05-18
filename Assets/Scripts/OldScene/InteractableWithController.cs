using UnityEngine;

public abstract class InteractableWithController : MonoBehaviour
{
    //
    // Must implement these
    //
    public abstract void OnControllerEnter();
    public abstract void OnControllerExit();
    
    //
    // Optional to implement
    //
    public virtual void OnInteractableGrabbed(Transform controller, ControllerEvents.ControllerInteractionEventArgs e, out bool isGrabbed)
    {
        // Only set to false because we have to because it is an out
        isGrabbed = false;
        return;
    }     
    
    public virtual void OnInteractableReleased(Transform controller, ControllerEvents.ControllerInteractionEventArgs e)
    {
        return;
    }
}
