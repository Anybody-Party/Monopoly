using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDiceButton : BaseActionButton
{
    protected override void Init()
    {
    }

    protected override void PerformOnClick()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
        UIEvents.RollDiceButtonTap?.Invoke();
    }
}
