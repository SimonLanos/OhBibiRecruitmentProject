using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<DefenderAI>();
        if (entity != null)
        {
            entity.GetComponent<CharacterEntity>().health += 5;
            //Score.AddScore(1000);
            Destroy(gameObject);
        }
    }
}
