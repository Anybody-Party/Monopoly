using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackCell : BaseCell
{
    private void Awake()
    {
        textOnCell.text = $"{DataManager.Instance.balanceData.GoBackSteps}";
    }

    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        character.StartMove(-DataManager.Instance.balanceData.GoBackSteps);
    }

    public override void OnCharacterCrossCell(Character character)
    {
    }

    public override void OnCharacterGoFromCell(Character character)
    {
        base.OnCharacterGoFromCell(character);
    }
}