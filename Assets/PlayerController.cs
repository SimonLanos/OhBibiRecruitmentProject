using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterEntity player;

    void FixedUpdate()
    {
            player.ShootNearestTarget();
    }
}
