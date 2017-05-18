using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class JumpObstacleKey : MonoBehaviour
{    
    [SerializeField]
    private ThirdFloorObstacle obstacleMover;

    [SerializeField]
    private Color touchedColor;
    [SerializeField]
    private MeshRenderer[] renderers;

    [SerializeField]
    private Transform rotationTip;
    [SerializeField]
    private Transform rotationRoot;
    
    private Transform masterHand;
    private Vector3 oldHandPosition;

    private Color originalColor;
    private bool grabbable;
    private bool inKeyHole;

    private KeyHole keyHole;

    private Transform floorToMove;
    private float totalDegreesRotated;

    void Awake()
    {
        // Get the original color from one of the parts
        originalColor = renderers[0].material.color;
        grabbable = true;
    }

    void OnTriggerEnter(Collider other)
    {
        keyHole = other.GetComponent<KeyHole>();
    }

    void OnTriggerExit(Collider other)
    {
        keyHole = null;
    }

    private void OnHandHoverBegin(Hand hand)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = touchedColor;
        }
    }

    private void OnHandHoverEnd(Hand hand)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = originalColor;
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        if (hand.controller != null && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            masterHand = hand.transform;
            oldHandPosition = masterHand.position;

            if (grabbable && !inKeyHole)
            {
                obstacleMover.StopMovement();

                hand.HoverLock(GetComponent<Interactable>());
                hand.AttachObject(gameObject, Hand.defaultAttachmentFlags & ~Hand.AttachmentFlags.SnapOnAttach);
            }
            else if (inKeyHole)
            {
                // do rotation
                hand.HoverLock(GetComponent<Interactable>());
                hand.AttachObject(gameObject, Hand.defaultAttachmentFlags & ~Hand.AttachmentFlags.SnapOnAttach & ~Hand.AttachmentFlags.ParentToHand);
            }
            else
            {
                masterHand = null;
            }
        }
    }

    void Update()
    {
        if (inKeyHole && masterHand)
        {            
            float angleDeltaDegrees = Vector3.Angle(oldHandPosition, masterHand.position);
            transform.Rotate(Vector3.forward, angleDeltaDegrees * 20f);

            totalDegreesRotated += angleDeltaDegrees;

            floorToMove.transform.Translate(Vector3.up * Time.deltaTime * angleDeltaDegrees);
        }

        if (masterHand)
        {
            oldHandPosition = masterHand.transform.position;
        }        
    }
    
    private void HandAttachedUpdate(Hand hand)
    {
        if (hand.controller != null && hand.controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {            
            if (keyHole && !keyHole.occupied)
            {
                // Put into the key hole
                transform.position = keyHole.lockedKeyPosition;
                transform.rotation = Quaternion.identity;
                inKeyHole = true;
                keyHole.occupied = true;
                floorToMove = keyHole.floorToMove;
            }

            masterHand = null;
            hand.HoverUnlock(GetComponent<Interactable>());
            hand.DetachObject(gameObject, false);

        }
    }    
}
