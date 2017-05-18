using System.Collections;
using UnityEngine;
using MMVR;

public class ElevatorPlatform : MonoBehaviour
{
    [SerializeField]
    private bool alwaysShowGizmo = false;

    [SerializeField]
    private InteractableButton elevatorButton;

    [SerializeField]
    private float liftHeight = 1f;
    [SerializeField]
    private float liftSpeed = 3f;

    [SerializeField]
    private Color activatedColor = Color.red;

    private Material mat;
    private Material buttonMat;
    private Color originalColor;

    private bool wasAlreadyLifted = false;

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
        if(elevatorButton)
        {
            buttonMat = elevatorButton.gameObject.GetComponent<MeshRenderer>().material;
        }        
        originalColor = mat.color;
    }

    public void LiftPlatform()
    {
        if(!wasAlreadyLifted)
        {
            wasAlreadyLifted = true;
            StartCoroutine(DoLiftPlatform());
        }
    }

    private IEnumerator DoLiftPlatform()
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y + liftHeight, transform.position.z);

        while(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * liftSpeed);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mat.color = activatedColor;
            buttonMat.color = activatedColor;

            elevatorButton.isActive = true;
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            mat.color = originalColor;
            buttonMat.color = originalColor;
        }        
    }

    void OnDrawGizmos()
    {
        if(alwaysShowGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                new Vector3(transform.position.x, transform.position.y + liftHeight / 2, transform.position.z),
                new Vector3(transform.localScale.x, transform.localScale.y + liftHeight, transform.localScale.z)
                );
        }
    }

    void OnDrawGizmosSelected()
    {
        if(!alwaysShowGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                new Vector3(transform.position.x, transform.position.y + liftHeight / 2, transform.position.z),
                new Vector3(transform.localScale.x, transform.localScale.y + liftHeight, transform.localScale.z)
                );
        }        
    }
}
