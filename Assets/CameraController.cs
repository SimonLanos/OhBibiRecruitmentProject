using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static CameraController instance;
    static Vector3 originMove;
    static Vector3 originPostionCamera;
    public float offsetPower = 0.01f;
    public float zoomSpeed = 1f;

    public Vector3 limitsMin;
    public Vector3 limitsMax;

    public Transform endOfRodTransform;

    private void Awake()
    {
        instance = this;
    }

    public static void Zoom(float delta)
    {

        instance.endOfRodTransform.position += delta * Camera.main.transform.forward * instance.zoomSpeed;
        /*
        instance.endOfRodTransform.position = new Vector3(Mathf.Clamp(instance.endOfRodTransform.position.x, instance.limitsMin.x, instance.limitsMax.x),
            Mathf.Clamp(instance.endOfRodTransform.position.y, instance.limitsMin.y, instance.limitsMax.y),
            Mathf.Clamp(instance.endOfRodTransform.position.z, instance.limitsMin.z, instance.limitsMax.z));
            */
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
