using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxCell : BaseCell
{
    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        character.WithdrawMoney(DataManager.Instance.balanceData.Tax);
    }

    public override void OnCharacterCrossCell(Character character)
    {
        
    }
}