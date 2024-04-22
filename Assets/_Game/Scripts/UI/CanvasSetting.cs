using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public void RetryButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }

    public void ContinueButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
