using RH.Utilities.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCellPanelButton : BaseActionButton
{
    private BaseCell buingCell;

    protected override void Init()
    {
        UIEvents.CellPanelShow.AddListener((_isShow, _cell) =>
        {
            buingCell = _cell;
            //SetShow(_isShow);
            UpdateButtonStatus();
        });
    }

    protected override void PerformOnClick()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        animator.SetTrigger("OnClick");
        GlobalEvents.BuyCell?.Invoke(buingCell.GetComponent<BuyableCell>());
        UIEvents.CellPanelShow?.Invoke(false, null);
    }

    private void UpdateButtonStatus()
    {
        if (buingCell)
            _button.interactable = DataManager.Instance.levelData.GetPlayerMoney() >= buingCell.GetComponent<BuyableCell>().Cost;
    }
}
