using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAI : MonoBehaviour
{
    CharacterEntity character;
    Transform target;
    public float probabilityOfTargetingDefendedUnits = 0.5f;
    public float updateRate = 1f;
    float updateOffset;
    float lastUpdateTime;


    public void Init(Vector3 position, Quaternion rotation)
    {
        if(character==null)
            character = GetComponent<CharacterEntity>();
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
        character.health = character.maxHealth;
        character.navMeshAgent.Warp(position);
        GetRandomTarget();
        updateOffset = Random.Range(0f, updateRate);
    }
    void FixedUpdate()
    {
        if (character.AttackContactWillLand())
        {
            character.AttackContact();
        }
        else
        {
            if (lastUpdateTime + updateRate + updateOffset < Time.time)
            {
                Transform closestTarget = character.GetClosestTargetTransformInVisionRange();
                if (closestTarget != null)
                {
                    target = closestTarget;
                }
                lastUpdateTime = Time.time;
            }
            if(target == null)
            {
                GetRandomTarget();
            }
            if (target != null)
            {
                character.MoveToPosition(target.position);
            }
        }
    }

    void GetRandomTarget()
    {
        if (DefendedAI.numberOfDefended > 0 && Random.Range(0f,1f)< probabilityOfTargetingDefendedUnits)
        {
            target = DefendedAI.instances[Random.Range(0, DefendedAI.instances.Count)].transform;
        }
        else if (DefenderAI.numberOfDefendersInGame > 0)
        {
            target = DefenderAI.instances[Random.Range(0, DefenderAI.instances.Count)].transform;
        }
    }

}
