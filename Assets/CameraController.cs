using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static Vector3 originMove;
    static Vector3 originPostionCamera;
    public float offsetPower = 0.01f;
    public float zoomSpeed = 1f;

    public Vector3 limitsMin;
    public Vector3 limitsMax;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            originMove = new Vector3(Input.mousePosition.x / Screen.currentResolution.width, Input.mousePosition.y / Screen.currentResolution.height);
            originPostionCamera = transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 offset = (new Vector3(Input.mousePosition.x / Screen.currentResolution.width, Input.mousePosition.y / Screen.currentResolution.height) - originMove) * offsetPower;
            offset = new Vector3(offset.x, 0f, offset.y);
            transform.position = originPostionCamera - offset;
        }
        transform.position += Input.GetAxis("Vertical") * Camera.main.transform.forward * zoomSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, limitsMin.x, limitsMax.x),
            Mathf.Clamp(transform.position.y, limitsMin.y, limitsMax.y),
            Mathf.Clamp(transform.position.z, limitsMin.z, limitsMax.z));
    }
}
