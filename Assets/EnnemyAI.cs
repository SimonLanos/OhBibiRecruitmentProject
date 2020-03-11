using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAI : MonoBehaviour
{
    CharacterEntity character;

    private void Start()
    {
        character = GetComponent<CharacterEntity>();
    }
    void FixedUpdate()
    {
        //character.ShootNearestTarget();
        if (character.SqrDistanceToNearestOpponent() < character.visionDistance* character.visionDistance / 4f)
        {
            character.ShootNearestTarget();
        }
        else
        {
            character.MoveTowardNearestOpponent();
        }
    }
}
