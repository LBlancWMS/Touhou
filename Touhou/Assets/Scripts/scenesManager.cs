using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scenesManager : MonoBehaviour
{
    public void playButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("level_1");
    }

    public void quitButtonPressed()
    {
        Application.Quit();
    }
}
