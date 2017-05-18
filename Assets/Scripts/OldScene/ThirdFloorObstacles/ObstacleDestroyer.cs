using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    [SerializeField]
    private ObstacleEmitter obstacleEmitter;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            obstacleEmitter.StartSpawning();
        }
        else if(other.GetComponent<ThirdFloorObstacle>())
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
