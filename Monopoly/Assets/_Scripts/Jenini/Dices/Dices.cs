using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Dices : MonoBehaviour
{
    [SerializeField] private Dice[] _cubes;
    [SerializeField] private ResultMode _resultMode;
    public bool Rolling { get; private set; }

    private void Awake()
    {
        foreach (var cube in _cubes)
            cube.Init(this);
    }

    internal void StartRoll()
    {
        Rolling = true;
        transform.DOLocalJump(transform.localPosition, 3.0f, 1, DataManager.Instance.balanceData.RollingTime).SetEase(Ease.InCubic);
        foreach (var cube in _cubes)
            cube.StartRoll();
    }

    internal void StopRoll()
    {
        Rolling = false;
        foreach (var cube in _cubes)
            cube.StopRoll();
    }

    internal int GetCubesValue()
    {
        return _resultMode switch
        {
            ResultMode.Add => _cubes.Sum(x => x.CurrentSide.Value),
            ResultMode.Highest => throw new NotImplementedException(),
            ResultMode.Lowest => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private enum ResultMode
    {
        Add = 0,
        Highest = 1,
        Lowest = 2
    }
}