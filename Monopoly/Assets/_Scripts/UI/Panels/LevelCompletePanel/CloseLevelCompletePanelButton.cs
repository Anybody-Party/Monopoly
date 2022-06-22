using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLevelCompletePanelButton : BaseActionButton
{
    protected override void Init()
    {
    }

    protected override void PerformOnClick()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        animator.SetTrigger("OnClick");
        UIEvents.LevelCompletePanelShow?.Invoke(false);
        UIEvents.CloseLevelCompletePanelButtonTap?.Invoke();
    }
}
