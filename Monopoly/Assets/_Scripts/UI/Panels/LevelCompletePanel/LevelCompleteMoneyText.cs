using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteMoneyText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeLevelCompleteMoneyText.AddListener(UpdateText);
    }
}
