using RH.Utilities.ComponentSystem;
using RH.Utilities.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBuySystem : BaseSystem
{
    public override void Dispose()
    {
        GlobalEvents.BuyCell.RemoveListener(BuyCell);
    }

    protected override void Init()
    {
        GlobalEvents.BuyCell.AddListener(BuyCell);
    }

    private void BuyCell(BuyableCell _cell)
    {
        _cell.SellOut();
    }
}
