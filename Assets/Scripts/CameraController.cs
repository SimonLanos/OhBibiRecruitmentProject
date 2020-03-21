using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static CameraController instance;
    public float offsetPower = 0.01f;
    public float zoomSpeed = 1f;

    public Transform endOfRodTransform;

    private void Awake()
    {
        instance = this;
    }

    public static void Zoom(float delta)
    {
        instance.endOfRodTransform.position += delta * Camera.main.transform.forward * instance.zoomSpeed;
    }

    public static void Move(Vector2 delta)
    {
        Vector3 offset = (delta.x*instance.transform.right + delta.y * instance.transform.forward) * instance.offsetPower;
        instance.transform.position += offset;
    }

    public static void Rotate(Vector3 euler)
    {
        instance.transform.rotation *= Quaternion.Euler(euler);
    }
}
