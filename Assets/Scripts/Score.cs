using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    static bool stopScore = false;
    static float timeStart;
    public static float timeSpent;
    void Start()
    {
        ResetScore();
    }

    public static void Stop()
    {
        stopScore = true;
        timeSpent = Time.time - timeStart;
        ScoreDisplayer.UpdateDisplayers();
    }

    public static void AddScore(int points)
    {
        if (!stopScore)
        {
            score += points;
            ScoreDisplayer.UpdateDisplayers();
        }
    }

    public static void ResetScore()
    {
        timeStart = Time.time;
        stopScore = false;
        score = 0;
        ScoreDisplayer.UpdateDisplayers();
    }
}
