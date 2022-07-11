using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartCell : BaseCell
{
    private void Start()
    {
        GlobalEvents.StartCellInit.Invoke(this);
        textOnCell.text = $"+{DataManager.Instance.balanceData.RewardForCompleteLoop}$";
        for (int i = 0; i < 40; i++)
            cellMoneyStack.AddItem(MoneyPool.Instance.UseItem());
    }

    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        StartCoroutine(GiveMoney(character));
    }

    public override void OnCharacterCrossCell(Character character)
    {
        StartCoroutine(GiveMoney(character));
    }

    public override void OnCharacterGoFromCell(Character character)
    {
        base.OnCharacterGoFromCell(character);
    }

    private IEnumerator GiveMoney(Character character)
    {
        while (cellMoneyStack.isRunning)
            yield return null;

        character.GiveMoney(DataManager.Instance.balanceData.RewardForCompleteLoop);
        cellMoneyStack.GiveMoneyToCharacterAtCell(character, DataManager.Instance.balanceData.RewardForCompleteLoop / 100);
    }
}
