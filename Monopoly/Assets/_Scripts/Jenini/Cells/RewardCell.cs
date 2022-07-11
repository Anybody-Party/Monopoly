using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCell : BaseCell
{
    private void Start()
    {
        for (int i = 0; i < 20; i++)
            cellMoneyStack.AddItem(MoneyPool.Instance.UseItem());

        textOnCell.text = $"+{DataManager.Instance.balanceData.RewardForRewardCell}$";
    }

    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        character.GiveMoney(DataManager.Instance.balanceData.RewardForRewardCell);
        cellMoneyStack.GiveMoneyToCharacterAtCell(character, DataManager.Instance.balanceData.RewardForRewardCell / 100);
    }

    public override void OnCharacterCrossCell(Character character)
    {
    }

    public override void OnCharacterGoFromCell(Character character)
    {
        base.OnCharacterGoFromCell(character);
    }
}