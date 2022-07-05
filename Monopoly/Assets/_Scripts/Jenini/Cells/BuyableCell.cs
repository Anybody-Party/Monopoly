using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableCell : BaseCell
{
    [SerializeField] private int _cost;
    [SerializeField] private MeshRenderer _coloredCellPart;
    [SerializeField] private Transform moneyPoint;
    private Color _defaultColor;
    private Character _owner;
    private int _moneyAtCell = 0;

    public int Cost => _cost;

    private void Start()
    {
        GlobalEvents.CharacterGameOver.AddListener(GameOverReset);
        textOnCell.text = $"{_cost}$";
        _defaultColor = _coloredCellPart.material.color;
    }

    public void SellOut()
    {
        _owner = _characterOnCell;
        textOnCell.text = $"{DataManager.Instance.mainData.CharacterCellNames[_owner.characterNum]}";
        Paint();
        _owner.Buy(this);
    }

    private void TakeTax(Character character)
    {
        character.WithdrawMoney(_cost * 2);
        _moneyAtCell += _cost * 2;
        cellMoneyStack.TakeMoneyFromCharacterAtCell(character, _cost * 2 / 100);
    }

    private void OwnerTakeMoney()
    {
        _owner.GiveMoney(_moneyAtCell);
        cellMoneyStack.GiveMoneyToCharacterAtCell(_owner, _moneyAtCell / 100);
    }

    private void Paint()
    {
        _coloredCellPart.material.color = _owner.PaintingColor;
        textOnCell.color = _owner.PaintingColor;
    }

    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);

        if (_owner == null)
            UIEvents.CellPanelShow.Invoke(true, this);
        else if (character != _owner)
            TakeTax(character);
        else
            OwnerTakeMoney();
    }

    public override void OnCharacterCrossCell(Character character)
    {
        if (character == _owner)
            OwnerTakeMoney();
    }

    private void GameOverReset(int _characterNum)
    {
        if (_owner.characterNum == _characterNum)
        {
            _coloredCellPart.material.color = _defaultColor;
            textOnCell.color = _defaultColor;
            _owner = null;
            _moneyAtCell = 0;
            cellMoneyStack.DeleteAll();
        }
    }
}