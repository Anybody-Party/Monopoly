using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailCell : BaseCell
{
    [SerializeField] private float _minStopTime;
    [SerializeField] private float _maxStopTime;

    public override void OnCharacterEnteredCell(Character character)
    {
        character.WaitBeforeRoll(Random.Range(_minStopTime, _maxStopTime));
    }
}