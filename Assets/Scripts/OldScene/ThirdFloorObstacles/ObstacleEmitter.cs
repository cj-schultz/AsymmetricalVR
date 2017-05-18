using System.Collections;
using UnityEngine;

public class ObstacleEmitter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstaclePool;

    [Space]
    [Header("Spawn Parameters")]
    [SerializeField]
    private float minSpawnRate = 3f;
    [SerializeField]
    private float maxSpawnRate = 5f;

    public void StartSpawning()
    {
        StartCoroutine("DoSpawning");
    }

    public void StopSpawning()
    {
        StopCoroutine("DoSpawning");
    }

    private IEnumerator DoSpawning()
    {
        while(true)
        {
            GameObject obstacle = obstaclePool[UnityEngine.Random.Range(0, obstaclePool.Length - 1)];
            Vector3 spawnPoint = new Vector3(transform.localPosition.x, obstacle.transform.position.y, obstacle.transform.position.z);
            Instantiate(obstacle, spawnPoint, Quaternion.identity);

            float waitTime = UnityEngine.Random.Range(minSpawnRate, maxSpawnRate);

            yield return new WaitForSeconds(waitTime);
        }        
    }
}
