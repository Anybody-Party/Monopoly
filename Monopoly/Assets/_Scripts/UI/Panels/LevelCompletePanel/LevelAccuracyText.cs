using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAccuracyText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeLevelAccuracyText.AddListener(UpdateText);
    }
}
