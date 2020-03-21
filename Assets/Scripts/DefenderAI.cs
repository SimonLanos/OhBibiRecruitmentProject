using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAI : MonoBehaviour
{
    public static int numberOfDefendersInGame = 0;
    CharacterEntity character;
    public static List<DefenderAI> instances = new List<DefenderAI>();
    Transform target;
    private void Start()
    {
        numberOfDefendersInGame++;
        instances.Add(this);
        character = GetComponent<CharacterEntity>();
    }

    void FixedUpdate()
    {
        Transform target = character.GetClosestTargetTransformInVisionRange();
        if (target != null)
        {
            character.ShootAt(target.position);
        }
    }

    private void OnDestroy()
    {
        DefenderAI.numberOfDefendersInGame--;
        instances.Remove(this);
        if (DefenderAI.numberOfDefendersInGame <= 0)
        {
            LevelManager.ShowGameOverScreen();
        }
    }
}
