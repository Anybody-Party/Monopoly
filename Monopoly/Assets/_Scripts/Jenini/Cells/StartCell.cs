using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCell : BaseCell
{
    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
    }

    public override void OnCharacterCrossCell(Character character)
    {
        character.GiveMoney(DataManager.Instance.balanceData.RewardForCompleteLoop);
        cellMoneyStack.GiveMoneyToCharacterAtCell(character, DataManager.Instance.balanceData.RewardForCompleteLoop / 100);
    }
}