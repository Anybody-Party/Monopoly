using RH.Utilities.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellPanel : BasePanel
{
    protected override void Init()
    {
        UIEvents.CellPanelShow.AddListener((_isShow, _cell) =>
        {
            SetShow(_isShow);
            if (_isShow) 
                UpdateInfo(_cell);
        });
    }

    private void UpdateInfo(BaseCell _cell)
    {
        UIEvents.ChangeCellPanelText?.Invoke($"BUY THIS CELL: {_cell.GetComponent<BuyableCell>().Cost}$");
    }
}
