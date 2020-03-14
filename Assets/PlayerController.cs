using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static CharacterEntity selectedCharacter;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Clicked " + hit.transform.name);
                if (selectedCharacter != null)
                {
                    Vector3 destination = new Vector3(hit.point.x, selectedCharacter.transform.position.y, hit.point.z);
                    selectedCharacter.MoveToPosition(destination);
                }
                selectedCharacter = hit.transform.GetComponent<CharacterEntity>();
            }
        }

    }
}
