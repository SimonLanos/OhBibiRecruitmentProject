using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<AllyAI>();
        if (entity != null)
        {
            Debug.Log("healed");
            entity.GetComponent<CharacterEntity>().health += 5;
            Destroy(gameObject);
        }
    }
}
