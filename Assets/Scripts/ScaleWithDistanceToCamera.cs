using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithDistanceToCamera : MonoBehaviour
{
    public float minSize = 2f;
    public float maxSize = 4f;
    public float refDistance = 30f;

    Transform mainCameraTransform;
    float scale; 
    float distanceToCamera;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        distanceToCamera = (transform.position - mainCameraTransform.position).magnitude;
        scale = distanceToCamera / refDistance * minSize;
        scale = Mathf.Clamp(scale, minSize, maxSize);
        transform.localScale = Vector3.one*scale;

    }
}
