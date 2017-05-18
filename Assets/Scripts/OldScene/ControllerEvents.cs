using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerEvents : MonoBehaviour
{
    public struct ControllerInteractionEventArgs
    {
        public uint controllerIndex;
        public Vector2 touchpadAxis;
    }

    // Button Events
    public delegate void ControllerInteractionEventHandler(object sender, ControllerInteractionEventArgs e);
    public event ControllerInteractionEventHandler TriggerPressed;
    public event ControllerInteractionEventHandler TriggerReleased;

    public event ControllerInteractionEventHandler TouchpadPressed;
    public event ControllerInteractionEventHandler TouchpadReleased;

    public event ControllerInteractionEventHandler TouchpadTouchStart;
    public event ControllerInteractionEventHandler TouchpadTouchEnd;

    public event ControllerInteractionEventHandler TouchpadAxisChanged;

    // Member variables
    [HideInInspector]
    public bool triggerPressed = false;
    [HideInInspector]
    public bool touchpadPressed = false;
    [HideInInspector]
    public bool touchpadTouched = false;
    [HideInInspector]
    public bool touchpadAxisChanged = false;

    public SteamVR_Controller.Device device { get; private set; }
    private uint controllerIndex;
    private SteamVR_TrackedObject trackedObj;

    private Vector2 touchpadAxis = Vector2.zero;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        controllerIndex = (uint)trackedObj.index;
        device = SteamVR_Controller.Input((int)controllerIndex);

        Vector2 currentTouchpadAxis = device.GetAxis();

        // Trigger
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            OnTriggerPressed(SetButtonEventArgs(ref triggerPressed, true));
        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            OnTriggerReleased(SetButtonEventArgs(ref triggerPressed, false));
        }

        // Touchpad press
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OnTouchpadPressed(SetButtonEventArgs(ref touchpadPressed, true));
        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OnTouchpadReleased(SetButtonEventArgs(ref touchpadPressed, false));
        }

        // Touchpad touch
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OnTouchpadTouchStart(SetButtonEventArgs(ref touchpadTouched, true));
        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OnTouchpadTouchEnd(SetButtonEventArgs(ref touchpadTouched, false));
        }

        // Touchpad Axis
        if (currentTouchpadAxis == touchpadAxis)
        {
            touchpadAxisChanged = false;
        }
        else
        {
            touchpadAxis = currentTouchpadAxis;
            OnTouchpadAxisChanged(SetButtonEventArgs(ref touchpadAxisChanged, true));
        }
    }

    private ControllerInteractionEventArgs SetButtonEventArgs(ref bool buttonState, bool value)
    {
        buttonState = value;

        ControllerInteractionEventArgs e;
        e.controllerIndex = controllerIndex;
        e.touchpadAxis = touchpadAxis;

        return e;
    }

    #region EVENT PUBLISHERS
    // Trigger
    private void OnTriggerPressed(ControllerInteractionEventArgs e)
    {
        if (TriggerPressed != null)
        {
            TriggerPressed(this, e);
        }
    }

    private void OnTriggerReleased(ControllerInteractionEventArgs e)
    {
        if (TriggerReleased != null)
        {
            TriggerReleased(this, e);
        }
    }

    // Touchpad
    private void OnTouchpadPressed(ControllerInteractionEventArgs e)
    {
        if (TouchpadPressed != null)
        {
            TouchpadPressed(this, e);
        }
    }

    private void OnTouchpadReleased(ControllerInteractionEventArgs e)
    {
        if (TouchpadReleased != null)
        {
            TouchpadReleased(this, e);
        }
    }

    private void OnTouchpadTouchStart(ControllerInteractionEventArgs e)
    {
        if (TouchpadTouchStart != null)
        {
            TouchpadTouchStart(this, e);
        }
    }

    private void OnTouchpadTouchEnd(ControllerInteractionEventArgs e)
    {
        if (TouchpadTouchEnd != null)
        {
            TouchpadTouchEnd(this, e);
        }
    }

    private void OnTouchpadAxisChanged(ControllerInteractionEventArgs e)
    {
        if (TouchpadAxisChanged != null)
        {
            TouchpadAxisChanged(this, e);
        }
    }

    #endregion
}
