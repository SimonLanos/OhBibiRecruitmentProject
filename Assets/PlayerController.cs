using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static CharacterEntity selectedCharacter;
    public Material selectedMaterial;
    public Material previousMaterial;
    public LayerMask selectableMask;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, selectableMask))
            {
                Debug.Log("Clicked " + hit.transform.name);
                if (selectedCharacter != null)
                {
                    selectedCharacter.GetComponent<Renderer>().material = previousMaterial;
                    Vector3 destination = new Vector3(hit.point.x, selectedCharacter.transform.position.y, hit.point.z);
                    selectedCharacter.MoveToPosition(destination);
                }
                selectedCharacter = hit.transform.GetComponentInParent<CharacterEntity>();
                if (selectedCharacter != null)
                {
                    previousMaterial = selectedCharacter.GetComponent<Renderer>().material;
                    selectedCharacter.GetComponent<Renderer>().material = selectedMaterial;
                }
            }
        }

    }
}
