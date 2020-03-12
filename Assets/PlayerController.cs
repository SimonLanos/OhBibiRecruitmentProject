using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static CharacterEntity selectedCharacter;
    static Vector3 originMove;
    static Vector3 originPostionCamera;
    public float offsetPower = 1;


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

            originMove = Input.mousePosition;
            originPostionCamera = Camera.main.transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 offset  = (Input.mousePosition - originMove) * offsetPower;
            offset = new Vector3(offset.x, 0f, offset.y);
            Camera.main.transform.position = originPostionCamera - offset;
        }
    }
}
