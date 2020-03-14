using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static Vector3 originMove;
    static Vector3 originPostionCamera;
    public float offsetPower = 0.01f;
    public float zoomSpeed = 1f;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            originMove = Input.mousePosition;
            originPostionCamera = transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 offset = (Input.mousePosition - originMove) * offsetPower;
            offset = new Vector3(offset.x, 0f, offset.y);
            transform.position = originPostionCamera - offset;
        }
        transform.position += Input.GetAxis("Vertical") * Camera.main.transform.forward * zoomSpeed;
    }
}
