using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToFixText : BaseText
{
    protected override void Init()
    {
        UIEvents.TutorialTapToFixTextShow.AddListener(SetShow);
    }
}
