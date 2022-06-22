using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteGetRewardButton : BaseActionButton
{
    protected override void Init()
    {
        UIEvents.LevelCompleteGetRewardButtonShow.AddListener(SetShow);
        UIEvents.LevelCompleteGetRewardButtonShow.AddListener((x) => { UpdateButtonStatus(); });
    }

    protected override void PerformOnClick()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        animator.SetTrigger("OnClick");
        UIEvents.LevelCompleteGetRewardButtonTap?.Invoke();
        UIEvents.LevelCompletePanelShow?.Invoke(false);
    }

    private void UpdateButtonStatus()
    {
        //_button.interactable = 
    }
}
