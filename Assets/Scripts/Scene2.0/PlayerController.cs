using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool allowJumping = true;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lookSensitivity = 5f;

    private PlayerMotor playerMotor;

    void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        // Movement
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 xVelocity = xMovement * transform.right;
        Vector3 zVelocity = zMovement * transform.forward;

        Vector3 velocity = (xVelocity + zVelocity) * speed;

        playerMotor.Move(velocity);

        // Check jump
        if(allowJumping && Input.GetKeyDown(KeyCode.Space))
        {
            playerMotor.JumpTriggered();
        }
    
        // Player rotation
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 playerRotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        playerMotor.Rotate(playerRotation);

        // Camera rotation
        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRot * lookSensitivity;

        playerMotor.RotateCamera(cameraRotationX);
    }
}
