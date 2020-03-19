using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenAI : MonoBehaviour
{
    static int numberOfCitizens = 0;

    private void Start()
    {
        numberOfCitizens++;
    }

    private void OnDestroy()
    {
        numberOfCitizens--;
        if (numberOfCitizens <= 0)
        {
            LevelManager.ShowGameOverScreen();
        }
    }
}
