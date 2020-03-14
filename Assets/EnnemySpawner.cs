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
            Vector3 proportionalSpawnPos = new Vector3(Random.Range(0, 2),Random.Range(0, 2), Random.Range(0f, 1f)) * 2f - Vector3.one;
            if (Random.Range(0, 2) == 0)
            {
                proportionalSpawnPos = new Vector3(proportionalSpawnPos.z, proportionalSpawnPos.y, proportionalSpawnPos.x);
            }
            EnnemyFactory.Create(transform.position+ Vector3.Scale(proportionalSpawnPos, SpawnExtent), Quaternion.identity);
            lastSpawnTime = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SpawnExtent * 2f);
    }
}
