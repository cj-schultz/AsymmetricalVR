using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera playerCam;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float cameraRotationLimit = 50f;

    private Rigidbody rb;

    private Vector3 velocity;
    private Vector3 rotation;
    private float cameraRotationX;
    private float currentCameraRotationX;

    private bool shouldJump = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rotation = transform.rotation.eulerAngles;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void JumpTriggered()
    {
        shouldJump = true;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    void FixedUpdate()
    {
        DoMovement();
        DoJump();
        DoRotation();
    }

    private void DoMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }        
    }

    private void DoJump()
    {
        if (shouldJump && rb.velocity.y == 0)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            shouldJump = false;
        }
    }

    private void DoRotation()
    {
        // Player rotation
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        // Camera rotation
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        playerCam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
