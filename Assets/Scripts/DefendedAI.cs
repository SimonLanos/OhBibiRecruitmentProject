using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendedAI : MonoBehaviour
{
    public static int numberOfDefended = 0;
    public Vector2 moveCoolDownRange;
    public LayerMask wallMask;
    CharacterEntity character;
    RaycastHit[] results = new RaycastHit[1];
    public Vector3 moveExtent = new Vector3(50, 10, 50);
    public static List<DefendedAI> instances = new List<DefendedAI>();
    private void Start()
    {
        instances.Add(this);
        character = GetComponent<CharacterEntity>();
        numberOfDefended++;
        StartCoroutine(WaitBeforeMovingAgainCoroutine());
    }
    
    IEnumerator WaitBeforeMovingAgainCoroutine()
    {
        yield return new WaitForSeconds(10f);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(moveCoolDownRange.x, moveCoolDownRange.y));
            SetRandomDestination();
            yield return null;
            while (character.navMeshAgent.hasPath)
            {
                yield return null;
            }
        }
    }

    void SetRandomDestination()
    {
        Vector3 destination = new Vector3(Random.Range(-moveExtent.x, moveExtent.x), moveExtent.y, Random.Range(-moveExtent.z, moveExtent.z));
        Ray ray = new Ray(destination, Vector3.down);
        if (Physics.RaycastNonAlloc(ray, results, 20f, wallMask) > 0)
        {
            character.MoveToPosition(results[0].point);
        }
        else
        {
            Debug.LogError("Raycast Missed");
        }
    }

    private void OnDestroy()
    {
        numberOfDefended--;
        instances.Remove(this);
        if (numberOfDefended <= 0)
        {
            LevelManager.ShowGameOverScreen();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, moveExtent * 2f);
    }
}
