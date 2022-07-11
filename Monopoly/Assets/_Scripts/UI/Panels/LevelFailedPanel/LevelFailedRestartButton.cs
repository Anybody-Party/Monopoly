using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailedRestartButton : BaseActionButton
{
    protected override void Init()
    {
    }

    protected override void PerformOnClick()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        animator.SetTrigger("OnClick");
        UIEvents.RestartButtonTap?.Invoke();
        UIEvents.LevelFailedPanelShow?.Invoke(false);
        UIEvents.LevelCompletePanelShow?.Invoke(false);
    }

    private void UpdateButtonStatus()
    {
    }
}
