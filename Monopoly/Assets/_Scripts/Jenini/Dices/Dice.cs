using System;
using System.Collections;
using Coroutines;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class Dice : MonoBehaviour
{
    [SerializeField] private float _valuesPerSecondSpeed = 1f;
    [SerializeField] private CubeSides _sides;
    [SerializeField] private bool _useVisual;

    [SerializeField]
    [Condition(nameof(_useVisual))]
    private DiceVisual _visual;

    private CoroutineObject _rollingRoutine;

    public CubeSide CurrentSide { get; private set; }

    public void Init(MonoBehaviour owner)
    {
        _rollingRoutine = new CoroutineObject(owner, RollingRoutine);
    }

    public void StartRoll()
    {
        _rollingRoutine.Start();
    }

    public void StopRoll()
    {
        _rollingRoutine.Stop();
    }

    private IEnumerator RollingRoutine()
    {
        var showTime = 1 / _valuesPerSecondSpeed;
        CurrentSide = GetRandomSideExceptCurrent();
        if (_useVisual)
            _visual.ShowSide(CurrentSide, showTime);
        yield return null;
        //var delay = new WaitForSeconds(showTime);
        //while (_rollingRoutine.Owner.enabled)
        //{
        //    CurrentSide = GetRandomSideExceptCurrent();
        //    if (_useVisual)
        //        _visual.ShowSide(CurrentSide, showTime);
        //    yield return delay;
        //}
    }

    private CubeSide GetRandomSideExceptCurrent()
    {
        CubeSide randomSide;
        do
        {
            var randomIndex = Random.Range(0, _sides.Sides.Length);
            randomSide = _sides.Sides[randomIndex];
        } while (randomSide.Value == CurrentSide.Value);

        return randomSide;
    }
}