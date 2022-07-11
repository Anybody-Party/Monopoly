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
    private CharacterAnimator characterAnimator;
    public bool isCanRoll;

    public Color PaintingColor => _paintingColor;

    private void Awake()
    {
        isCanRoll = true;
        characterAnimator = GetComponent<CharacterAnimator>();
        GlobalEvents.CharacterGameOver.AddListener(GameOver);
        if (isRealPlayer)
        {
            if (isCanRoll)
                UIEvents.CellPanelShow?.Invoke(false, null);
            UIEvents.RollDiceButtonTap.AddListener(TryRollingDices);
        }
    }

    public void JailWaitBeforeRoll(float time)
    {
        isCanRoll = false;
        DOTween.Sequence().AppendInterval(time).OnComplete(() => { isCanRoll = true; });
    }

    public void Buy(BuyableCell cell)
    {
        WithdrawMoney(cell.Cost, true);
    }

    public void WithdrawMoney(int money, bool isBuying = false)
    {
        GlobalEvents.AddMoney.Invoke(characterNum, -money);
        minusText.text = $"-{money}$";
        minusTextAnimator.SetTrigger("IsShow");

        if (isBuying)
            characterAnimator.Happy();
        else
            characterAnimator.Sad();
    }

    public void GiveMoney(int money)
    {
        if (money > 0)
        {
            GlobalEvents.AddMoney.Invoke(characterNum, money);
            plusText.text = $"+{money}$";
            plusTextAnimator.SetTrigger("IsShow");
            characterAnimator.Happy();
        }
    }

    public void TryRollingDices()
    {
        if (!isCanRoll)
            return;
        else if (isRealPlayer)
            UIEvents.CellPanelShow?.Invoke(false, null);

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

    public void SetStartCell(BaseCell _cell)
    {
        _currentCell = _cell;
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
        transform.DOLookAt(targetPosition, DataManager.Instance.balanceData.JumpToTileTime, AxisConstraint.Y);
        characterAnimator.Jump();
        yield return new WaitForSeconds(DataManager.Instance.balanceData.JumpToTileTime);
        stepsCounter--;
        _currentCell.OnCharacterGoFromCell(this);
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
        characterAnimator.Jump();
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

    private void GameOver(int _characterNum)
    {
        if (characterNum == _characterNum)
        {
            characterAnimator.Fall();
        }
    }
}