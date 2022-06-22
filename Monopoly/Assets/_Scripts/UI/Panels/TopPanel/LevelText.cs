using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeLevelText.AddListener(UpdateText);
    }
}
