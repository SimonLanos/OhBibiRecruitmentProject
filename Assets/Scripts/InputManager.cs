using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static CharacterEntity selectedCharacter;
    public Material selectedMaterial;
    public Material previousMaterial;
    public LayerMask selectableMask;
    public LayerMask wallMask;
    float previousDistanceBetweenTouches;
    Vector2 previousVectorBetweenTouches;
    Vector2 previousMousePosition;

    float distanceBetweenTouches;
    Vector2 vectorBetweenTouches;

    private void Update()
    {
        ControlCharacters();

#if !UNITY_EDITOR
        ControlCamera();
#endif

#if UNITY_EDITOR
        //SIMULATE CAMERA CONTROLS
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float distanceDeltaSimulate = vertical * 0.01f;

        float angledeltaSimulate = horizontal;

        CameraController.Rotate(new Vector3(0, angledeltaSimulate, 0));
        CameraController.Zoom(distanceDeltaSimulate);
        if (Input.GetMouseButtonDown(0))
            previousMousePosition = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            CameraController.Move(-(new Vector2(Input.mousePosition.x, Input.mousePosition.y) - previousMousePosition) / Mathf.Min(Screen.height, Screen.width));
            previousMousePosition = Input.mousePosition;
        }
#endif
    }


    public void ControlCharacters()
    {
        if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (selectedCharacter == null)
                {
                    if (Physics.Raycast(ray, out hit, 1000, selectableMask))
                    {
                        selectedCharacter = hit.transform.GetComponentInParent<CharacterEntity>();
                        if (selectedCharacter != null)
                        {
                            selectedCharacter.ShowSelection();
                        }

                    }
                }
                else
                {
                    if (Physics.Raycast(ray, out hit, 1000, wallMask))
                    {

                        Vector3 destination = new Vector3(hit.point.x, selectedCharacter.transform.position.y, hit.point.z);
                        selectedCharacter.MoveToPosition(destination);
                        selectedCharacter.HideSelection();
                        selectedCharacter = null;
                    }
                }
            }
        }
    }
    public void ControlCamera()
    {
        if (Input.touchCount > 1)
        {
            distanceBetweenTouches = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude / Mathf.Min(Screen.height, Screen.width);
            vectorBetweenTouches = (Input.GetTouch(0).position - Input.GetTouch(1).position);


            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
            {
                previousDistanceBetweenTouches = distanceBetweenTouches;
                previousVectorBetweenTouches = vectorBetweenTouches;
            }
        }
        float angledelta = Vector2.SignedAngle(previousVectorBetweenTouches, vectorBetweenTouches);
        float distanceDelta = distanceBetweenTouches - previousDistanceBetweenTouches;

        CameraController.Move(-Input.GetTouch(0).deltaPosition / Mathf.Min(Screen.height, Screen.width));
        CameraController.Rotate(new Vector3(0, angledelta, 0));
        CameraController.Zoom(distanceDelta);
        previousDistanceBetweenTouches = distanceBetweenTouches;
        previousVectorBetweenTouches = vectorBetweenTouches;
    }
}
