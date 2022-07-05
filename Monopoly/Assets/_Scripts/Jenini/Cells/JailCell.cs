using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailCell : BaseCell
{
    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        character.JailWaitBeforeRoll(DataManager.Instance.balanceData.JailStopTime);
    }

    public override void OnCharacterCrossCell(Character character)
    {
    }
}