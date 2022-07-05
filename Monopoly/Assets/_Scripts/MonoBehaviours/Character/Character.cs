using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public bool isRealPlayer;
    [SerializeField] public int characterNum;
    [SerializeField] public MoneyStack moneyStack;
    [SerializeField] private Dices _dices;
    [SerializeField] private Color _paintingColor;

    [Header("Money")]
    [SerializeField] private Animator plusTextAnimator;
    [SerializeField] private TextMeshPro plusText;
    [SerializeField] private Animator minusTextAnimator;
    [SerializeField] private TextMeshPro minusText;

    private BaseCell _currentCell;
    private bool isCanRoll;

    public Color PaintingColor => _paintingColor;

    private void Awake()
    {
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
        minusText.text = $"-{money}$";
        minusTextAnimator.SetTrigger("IsShow");
    }

    public void GiveMoney(int money)
    {
        GlobalEvents.AddMoney.Invoke(characterNum, money);
        plusText.text = $"+{money}$";
        plusTextAnimator.SetTrigger("IsShow");
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
        _currentCell = Board.Instance.GetCellBySteps(_currentCell, 1);
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
        _currentCell = Board.Instance.GetCellBySteps(_currentCell, -1);
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