using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSetting : UICanvas
{
    public void RetryButton()
    {
        Close(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinueButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
