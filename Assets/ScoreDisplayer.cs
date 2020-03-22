using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplayer : MonoBehaviour
{
    static List<ScoreDisplayer> instances = new List<ScoreDisplayer>();
    public Text killCount;
    public Text timeSpent;
    public TextMeshProUGUI killCountTMPro;
    public TextMeshProUGUI timeSpentTMPro;
    // Start is called before the first frame update
    void Start()
    {
        instances.Add(this);
        UpdateDisplayer();
    }

    private void OnDestroy()
    {
        instances.Remove(this);
    }

    public void UpdateDisplayer()
    {
        if (killCount != null)
        {
            killCount.text = Score.score.ToString();
        }
        if (killCountTMPro != null)
        {
            killCountTMPro.text = Score.score.ToString();
        }
        if (timeSpent != null)
        {
            timeSpent.text = ((int)Score.timeSpent).ToString();
        }
        if (timeSpentTMPro != null)
        {
            timeSpentTMPro.text = ((int)Score.timeSpent).ToString();
        }
    }

    public static void UpdateDisplayers()
    {
        for(int i = 0; i < instances.Count; i++)
        {
            ScoreDisplayer.instances[i].UpdateDisplayer();
        }
    }

}
