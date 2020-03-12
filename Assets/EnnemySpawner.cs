using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public Vector3 SpawnExtent;
    public float spawnCoolDown;
    float lastSpawnTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawnTime + spawnCoolDown)
        {
            EnnemyFactory.Create(transform.position+ new Vector3(Random.Range(-SpawnExtent.x, SpawnExtent.x), Random.Range(-SpawnExtent.y, SpawnExtent.y), Random.Range(-SpawnExtent.z, SpawnExtent.z)), Quaternion.identity);
            lastSpawnTime = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SpawnExtent * 2f);
    }
}
