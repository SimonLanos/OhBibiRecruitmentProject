using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenAI : MonoBehaviour
{
    static int numberOfCitizens = 0;
    public Vector2 moveCoolDownRange;
    float lastMoveTime;
    public LayerMask wallMask;
    CharacterEntity character;
    Coroutine coroutine;

    private void Start()
    {
        character = GetComponent<CharacterEntity>();
        numberOfCitizens++;
        lastMoveTime = Time.time;
        StartCoroutine(WaitBeforeMovingAgainCoroutine());
    }
    
    IEnumerator WaitBeforeMovingAgainCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(moveCoolDownRange.x, moveCoolDownRange.y));
            SetRandomDestination();
            yield return null;
            while (character.navMeshAgent.hasPath)
            {
                yield return null;
            }
            Debug.Log("<color=red>reached destination</color>");
        }
    }

    void SetRandomDestination()
    {
        Debug.Log("<color=red>go to  destination</color>");
        Vector3 destination = new Vector3(Random.Range(-50f, 50f), 10f, Random.Range(-50f, 50f));
        RaycastHit[] results = new RaycastHit[1];
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
        numberOfCitizens--;
        if (numberOfCitizens <= 0)
        {
            LevelManager.ShowGameOverScreen();
        }
    }
}
