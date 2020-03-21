using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public Vector3 SpawnExtent;
    public Vector2Int numberOfENnemiesRange;
    public Vector2 spawnCoolDownRange;
    public float spawnCoolDown;
    public float timeToReachMaxDifficulty;
    float lastSpawnTime;
    float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawnTime + spawnCoolDown)
        {
            int numberOfEnnemies = Random.Range(numberOfENnemiesRange.x, numberOfENnemiesRange.y+1);
            for (int i = 0; i < numberOfEnnemies; i++)
            {
                Vector3 proportionalSpawnPos = new Vector3(Random.Range(0, 2), 0f, Random.Range(0f, 1f)) * 2f - Vector3.one;
                if (Random.Range(0, 2) == 0)
                {
                    proportionalSpawnPos = new Vector3(proportionalSpawnPos.z, proportionalSpawnPos.y, proportionalSpawnPos.x);
                }
                AttackerFactory.Create(transform.position + Vector3.Scale(proportionalSpawnPos, SpawnExtent), Quaternion.identity);
            }
            spawnCoolDown = Mathf.Lerp(spawnCoolDownRange.y, spawnCoolDownRange.x, (Time.time-startTime)/timeToReachMaxDifficulty);
            lastSpawnTime = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SpawnExtent * 2f);
    }
}
