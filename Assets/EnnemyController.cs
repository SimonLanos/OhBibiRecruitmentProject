using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    public CharacterEntity character;

    private void Start()
    {
        character = GetComponent<CharacterEntity>();
    }
    void FixedUpdate()
    {
            character.ShootNearestTarget();
        character.MoveTowardNearestOpponent();
    }
}
