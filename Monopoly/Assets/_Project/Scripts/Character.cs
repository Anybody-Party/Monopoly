using System;
using DG.Tweening;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Dices _dices;
    [SerializeField] private BaseCell _startCell;
    [SerializeField] private Color _paintingColor;
    private BaseCell _currentCell;

    public Color PaintingColor => _paintingColor;

    private void Awake()
    {
        _currentCell = _startCell;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _dices.Rolling)
        {
            StopRollingDices();
        }
    }

    public void WaitBeforeRoll(float time)
    {
        DOTween.Sequence().AppendInterval(time).OnComplete(StartRollingDices);
    }

    public void Buy(BuyableCell cell)
    {
        WithdrawMoney(cell.Cost);
        StartRollingDices();
    }

    public void WithdrawMoney(float money)
    {
        // todo: withdraw money
        // if less then 0 we lose
    }

    private void StartRollingDices()
    {
        _dices.StartRoll();
    }

    private void StopRollingDices()
    {
        _dices.StopRoll();
        var value = _dices.GetCubesValue();
        Move(value);
    }

    public void Move(int steps)
    {
        _currentCell = _board.GetCellBySteps(_currentCell, steps);
        var targetPosition = _currentCell.CharacterPoint.position;
        targetPosition.x = transform.position.x;
        transform.DOMove(targetPosition, 0.5f).OnComplete(() => _currentCell.OnCharacterEnteredCell(this));
    }
}