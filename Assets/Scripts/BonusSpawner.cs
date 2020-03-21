using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public Vector3 SpawnExtent;
    public float spawnCoolDown;
    float lastSpawnTime;
    public Bonus bonusPrefab;
    public LayerMask wallMask;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawnTime + spawnCoolDown)
        {
            Vector3 spawnPos =Vector3.Scale(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), SpawnExtent);
            RaycastHit[] results = new RaycastHit[1];
            Ray ray = new Ray(spawnPos, Vector3.down);
            if (Physics.RaycastNonAlloc(ray, results, SpawnExtent.y * 2f, wallMask) > 0)
            {
                Instantiate(bonusPrefab, results[0].point+Vector3.up, Quaternion.identity);
                lastSpawnTime = Time.time;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SpawnExtent * 2f);
    }
}
