using RH.Utilities.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFailedPanel : BasePanel
{
    protected override void Init()
    {
        UIEvents.LevelFailedPanelShow.AddListener(SetShow);
    }
}
