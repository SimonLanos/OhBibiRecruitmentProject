using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAI : MonoBehaviour
{
    CharacterEntity character;
    Transform target;


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
        else if(!character.MoveTowardNearestOpponent())
        {
            if(target == null)
            {
                var cble = FindObjectOfType<CitizenAI>();
                if(cble)
                target = cble.transform;
            }
            if (target != null)
            {
                character.MoveToPosition(target.position);
            }
        }
    }
}
