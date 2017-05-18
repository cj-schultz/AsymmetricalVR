using UnityEngine;

public class PlayerCheckpointManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform[] checkpoints;

    public static int CurrentCheckpoint;

    public void SpawnAtCheckpoint()
    {
        player.transform.position = checkpoints[CurrentCheckpoint].position;
    }
}
