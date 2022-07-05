using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackCell : BaseCell
{
    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        character.StartMove(-DataManager.Instance.balanceData.GoBackSteps);
    }

    public override void OnCharacterCrossCell(Character character)
    {
    }
}