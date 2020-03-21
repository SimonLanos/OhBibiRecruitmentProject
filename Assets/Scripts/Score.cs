using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    static TextMeshProUGUI textMeshPro;
    static bool stopScore = false;
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public static void Stop(){
        stopScore = true;
    }

    public static void AddScore(int points)
    {
        if (!stopScore)
        {
            score += points;
            UpdateScore();
        }
    }

    public static void ResetScore()
    {
        stopScore = false;
        score = 0;
        UpdateScore();
    }

    static void UpdateScore()
    {
        if (textMeshPro != null)
            textMeshPro.text = score.ToString();
    }
}
