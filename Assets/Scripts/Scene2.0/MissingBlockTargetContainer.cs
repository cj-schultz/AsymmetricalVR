using System.Collections;
using UnityEngine;

public class MissingBlockTargetContainer : MonoBehaviour
{
    [SerializeField]
    private float snapSpeed = 10f;

    private MeshRenderer r;

    private MissingBridgeBlock missingBlock;

    void Awake()
    {
        r = GetComponent<MeshRenderer>();
        r.enabled = false;
    }

    public IEnumerator SnapBlockToContainer(Transform block)
    {
        while(block.position != transform.position || block.rotation != transform.rotation)
        {
            block.position = Vector3.MoveTowards(block.position, transform.position, Time.deltaTime * snapSpeed);
            block.rotation = Quaternion.Slerp(block.rotation, transform.rotation, Time.deltaTime * snapSpeed);
            yield return null;
        }
        
        block.GetComponent<BoxCollider>().isTrigger = false;

        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        MissingBridgeBlock temp = other.GetComponent<MissingBridgeBlock>();

        if(temp)
        {
            missingBlock = temp;
            missingBlock.container = this;
            r.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(missingBlock)
        {
            missingBlock.container = null;
            missingBlock = null;            
            r.enabled = false;
        }
    }
}
