using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static LevelManager instance;
    public GameObject gameOverScreen;
    static Coroutine slowingDown = null;

    private void Awake()
    {
        instance = this;
    }

    public static void ShowGameOverScreen()
    {
        instance.gameOverScreen.SetActive(true);

        Debug.Log("GameOver\n" + Score.score);
        slowingDown  = instance.StartCoroutine(slowDownTime(0.75f));
        Score.ResetScore();
    }

    public void Restart()
    {
        if (slowingDown != null)
        {
            instance.StopCoroutine(slowingDown);
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    static IEnumerator slowDownTime(float duration)
    {
        float startTime = Time.time;
        while (startTime + duration > Time.time)
        {
            Time.timeScale = 1f-(Time.time - startTime) / duration;
            yield return null;
        }

        Time.timeScale = 0f;
    }
}
