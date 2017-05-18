using UnityEngine;

public class KeyHole : MonoBehaviour
{    
    public Transform floorToMove;
    public Vector3 lockedKeyPosition;

    [HideInInspector]
    public bool occupied = false;
}
