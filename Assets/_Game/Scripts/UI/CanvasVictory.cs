using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    public void RetryButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }

    public void NextButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
