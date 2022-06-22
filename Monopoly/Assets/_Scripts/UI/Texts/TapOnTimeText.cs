using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapOnTimeText : BaseText
{
    protected override void Init()
    {
        UIEvents.TutorialTapOnTimeTextShow.AddListener(SetShow);
    }
}
