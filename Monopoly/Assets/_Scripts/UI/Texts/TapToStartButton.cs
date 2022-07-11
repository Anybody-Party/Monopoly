using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStartButton : BaseActionButton
{
    protected override void Init()
    {
        UIEvents.TapToStartTextShow.AddListener(SetShow);
    }

    protected override void PerformOnClick()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
        UIEvents.TapToStartTextShow.Invoke(false);
    }
}
