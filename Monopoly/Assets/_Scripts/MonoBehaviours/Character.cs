using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public bool isRealPlayer;
    [SerializeField] public int characterNum;
    [SerializeField] private Board _board;
    [SerializeField] private Dices _dices;
    [SerializeField] private BaseCell _startCell;
    [SerializeField] private Color _paintingColor;
    private BaseCell _currentCell;
    private bool isCanRoll;

    public Color PaintingColor => _paintingColor;

    private void Awake()
    {
        _currentCell = _startCell;
        isCanRoll = true;
        if (isRealPlayer)
            UIEvents.RollDiceButtonTap.AddListener(TryRollingDices);
    }

    public void JailWaitBeforeRoll(float time)
    {
        DOTween.Sequence().AppendInterval(time).OnComplete(() => { isCanRoll = true; });
    }

    public void Buy(BuyableCell cell)
    {
        WithdrawMoney(cell.Cost);
    }

    public void WithdrawMoney(int money)
    {
        GlobalEvents.AddMoney.Invoke(characterNum, -money);
    }

    public void GiveMoney(int money)
    {
        GlobalEvents.AddMoney.Invoke(characterNum, money);
    }

    private void TryRollingDices()
    {
        if (!isCanRoll)
            return;
        _dices.StartRoll();
        isCanRoll = false;
        DOTween.Sequence().AppendInterval(DataManager.Instance.balanceData.RollingTime).OnComplete(StopRollingDices);
    }

    private void StopRollingDices()
    {
        _dices.StopRoll();
        var value = _dices.GetCubesValue();
        StartMove(value);
    }

    public void StartMove(int steps)
    {
        StartCoroutine(steps > 0 ? MovingForward(steps) : MovingBackward(steps));
    }

    private IEnumerator MovingForward(int stepsCounter)
    {
        _currentCell = _board.GetCellBySteps(_currentCell, 1);
        var targetPosition = _currentCell.GetCharacterPoint(characterNum).position;
        transform.DOMove(targetPosition, DataManager.Instance.balanceData.JumpToTileTime);
        yield return new WaitForSeconds(DataManager.Instance.balanceData.JumpToTileTime);
        stepsCounter--;
        if (stepsCounter > 0)
        {
            _currentCell.OnCharacterCrossCell(this);
            StartCoroutine(MovingForward(stepsCounter));
        }
        else
        {
            isCanRoll = true;
            _currentCell.OnCharacterEnteredCell(this);
        }
    }
    
    private IEnumerator MovingBackward(int stepsCounter)
    {
        _currentCell = _board.GetCellBySteps(_currentCell, -1);
        var targetPosition = _currentCell.GetCharacterPoint(characterNum).position;
        transform.DOMove(targetPosition, DataManager.Instance.balanceData.JumpToTileTime);
        yield return new WaitForSeconds(DataManager.Instance.balanceData.JumpToTileTime);
        stepsCounter++;
        if (stepsCounter < 0)
        {
            _currentCell.OnCharacterCrossCell(this);
            StartCoroutine(MovingBackward(stepsCounter));
        }
        else
        {
            isCanRoll = true;
            _currentCell.OnCharacterEnteredCell(this);
        }
    }
}