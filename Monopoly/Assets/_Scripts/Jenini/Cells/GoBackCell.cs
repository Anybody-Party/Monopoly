using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackCell : BaseCell
{
    [SerializeField] private int _minSteps;
    [SerializeField] private int _maxSteps;

    public override void OnCharacterEnteredCell(Character character)
    {
        character.Move(-Random.Range(_minSteps, _maxSteps + 1));
    }
}