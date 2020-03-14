using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplayer : MonoBehaviour
{
    int frameCount = 0;
    float dt = 0f;
    int fps = 0;
    public float updateRate = 1f; 
    Text text;
    int minFps = int.MaxValue;
    int maxFps;
    private void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1f / updateRate)
        {
            fps = Mathf.FloorToInt(frameCount / dt);
            frameCount = 0;
            dt -= 1f / updateRate;
            if (fps < minFps)
                minFps = fps;
            if (fps > maxFps)
                maxFps = fps;
            text.text = fps.ToString() + "/" + minFps.ToString() + "/" + maxFps.ToString();
        }
    }
}
