using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAI : MonoBehaviour
{
    public static int numberOfAlliesInGame = 0;
    CharacterEntity character;

    private void Start()
    {
        numberOfAlliesInGame++;
           character = GetComponent<CharacterEntity>();
    }
    void FixedUpdate()
    {
        character.ShootNearestTarget();
    }
}
