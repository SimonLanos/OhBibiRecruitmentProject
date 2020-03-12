using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    static float strength;
    public float damp;
    public float speed;
    public float amplitude;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        strength *= damp;
        offset = new Vector3(Mathf.PerlinNoise((Time.time * speed) % 100f, 0.2f), 0.5f, Mathf.PerlinNoise(0.3f,(Time.time * speed) % 100f));
        offset = (offset*2f - Vector3.one);
            offset*= amplitude*Mathf.Log(1+strength);
        Camera.main.transform.position = transform.position + offset;
        
    }

    public static void Shake(float value)
    {
        strength += value;
    }
}
