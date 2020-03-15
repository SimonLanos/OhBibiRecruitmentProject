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
        if (character.AttackContactWillLand())
        {
            character.AttackContact();
        }
        else
        {
            character.MoveTowardNearestOpponent();
        }
    }
}
