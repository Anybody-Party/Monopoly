using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxCell : BaseCell
{
    private StartCell startCell;

    private void Awake()
    {
        GlobalEvents.StartCellInit.AddListener((x) => { startCell = x; });
        textOnCell.text = $"-{DataManager.Instance.balanceData.Tax}$";
    }

    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        character.WithdrawMoney(DataManager.Instance.balanceData.Tax);
        startCell.cellMoneyStack.TakeMoneyFromCharacterAtCell(character, (DataManager.Instance.balanceData.Tax / 100));
    }

    public override void OnCharacterCrossCell(Character character)
    {
    }

    public override void OnCharacterGoFromCell(Character character)
    {
        base.OnCharacterGoFromCell(character);
    }
}