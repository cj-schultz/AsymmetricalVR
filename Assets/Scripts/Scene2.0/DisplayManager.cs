using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    void Awake()
    {
        UnityEngine.VR.VRSettings.showDeviceView = false;
    }
}
