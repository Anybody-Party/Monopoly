using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DiceVisual
{
    [SerializeField] private Text _textValue;
    [SerializeField] private Transform _model;

    public void ShowSide(CubeSide cubeSide, float duration)
    {
        _textValue.text = cubeSide.Value.ToString();
        _model.DORotate(cubeSide.Rotation, duration);
    }
}