using UnityEngine;

public class ThirdFloorObstacle : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private bool shouldMove = true;

    void Update()
    {
        if(shouldMove)
        {
            // @Note : Left is forward on the third floor
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }        
    }

    public void StopMovement()
    {
        shouldMove = false;
    }
}
