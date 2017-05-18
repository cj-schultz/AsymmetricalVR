using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class SlidingObstacle : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor = Color.blue;

    private Material mat;
    private Color originalColor;

    private Hand grabbedHand;
    private float lastGrabbedAtPosition;
    private bool grabbed;

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
        originalColor = mat.color;
    }    

    private void OnHandHoverBegin(Hand hand)
    {
        mat.color = hoverColor;
    }

    private void OnHandHoverEnd(Hand hand)
    {
        mat.color = originalColor;

        if (grabbed)
        {
            hand.DetachObject(gameObject);
            grabbed = false;
        }
    }

    private void HandHoverUpdate(Hand hand)
    {                
        if (!grabbed && (hand.controller != null && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)))
        {
            lastGrabbedAtPosition = hand.transform.position.z;
            grabbed = true;
            grabbedHand = hand;
        }
        else if (grabbed && (hand.controller != null && hand.controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)))
        {
            grabbed = false;
        } 
    }
    
    void Update()
    {
        if(grabbed)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (grabbedHand.transform.position.z - lastGrabbedAtPosition));
            lastGrabbedAtPosition = grabbedHand.transform.position.z;
        }
    }
    
    private void HandAttachedUpdate(Hand hand)
    {        
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (hand.transform.position.z - lastGrabbedAtPosition));
        lastGrabbedAtPosition = hand.transform.position.z;
    }
}
