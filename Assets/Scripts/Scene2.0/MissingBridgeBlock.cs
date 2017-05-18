using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class MissingBridgeBlock : MonoBehaviour
{
    [HideInInspector]
    public bool readyForInteraction = false;

    [HideInInspector]
    public MissingBlockTargetContainer container;

    private bool isBeingGrabbed;
    private uint controllerIndex;
    
    private void HandHoverUpdate(Hand hand)
    {
        if(readyForInteraction && hand.controller != null && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            hand.HoverLock(GetComponent<Interactable>());
            hand.AttachObject(gameObject, Hand.defaultAttachmentFlags & ~Hand.AttachmentFlags.SnapOnAttach);          
        }
    }

    private void HandAttachedUpdate(Hand hand)
    {
        if(hand.controller != null && hand.controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            hand.HoverUnlock(GetComponent<Interactable>());
            hand.DetachObject(gameObject, false);

            if (container)
            {
                readyForInteraction = false; // Deactivate this object to be interacted with
                StartCoroutine(container.SnapBlockToContainer(transform));
            }
        }        
    }
}
