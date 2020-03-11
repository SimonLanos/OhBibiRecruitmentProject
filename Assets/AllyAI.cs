using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAI : MonoBehaviour
{
    CharacterEntity character;

    private void Start()
    {
        character = GetComponent<CharacterEntity>();
    }
    void FixedUpdate()
    {
        character.ShootNearestTarget();
    }
}
