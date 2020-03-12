using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score = 0;
    static TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public static void AddScore(int points)
    {
        score += points;
        text.text = score.ToString();
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
