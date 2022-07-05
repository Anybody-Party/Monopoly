using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPanelText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeCellPanelText.AddListener(UpdateText);
    }
}
