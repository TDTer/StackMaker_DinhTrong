using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>();
    }
}
