using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableCell : BaseCell
{
    [SerializeField] private int _cost;
    [SerializeField] private MeshRenderer _coloredCellPart;
    private Character _owner;
    private int _money;

    public int Cost => _cost;

    private void SellOut(Character owner)
    {
        _owner = owner;
        Paint();
        _owner.Buy(this);
    }

    private void TakeTax(Character character)
    {
        // take money from character
    }

    private void GiveMoney()
    {
        // give _money to owner
    }

    private void Paint()
    {
        _coloredCellPart.material.color = _owner.PaintingColor;
    }

    public override void OnCharacterEnteredCell(Character character)
    {
        if (_owner == null)
            SellOut(character);
        else if (character != _owner)
            TakeTax(character);
        else
            GiveMoney();
    }
}