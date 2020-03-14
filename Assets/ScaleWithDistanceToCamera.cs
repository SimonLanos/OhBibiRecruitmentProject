using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithDistanceToCamera : MonoBehaviour
{
    public float minSize = 2f;
    public float maxSize = 4f;
    public float refDistance = 30f;

    private void Update()
    {
        float distToCam = (transform.position - Camera.main.transform.position).magnitude;
        float scale = distToCam / refDistance * minSize;
        scale = Mathf.Clamp(scale, minSize, maxSize);
        transform.localScale = Vector3.one*scale;

    }
}
