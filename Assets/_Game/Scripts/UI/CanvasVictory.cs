using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasVictory : UICanvas
{
    public void RetryButton()
    {
        Close(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextButton()
    {
        Close(0);
        LevelManager.currentLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
