using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxCell : BaseCell
{
    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        character.WithdrawMoney(DataManager.Instance.balanceData.Tax);
        cellMoneyStack.TakeMoneyFromCharacterAtCell(character, DataManager.Instance.balanceData.Tax / 100);
    }

    public override void OnCharacterCrossCell(Character character)
    {
        
    }
}