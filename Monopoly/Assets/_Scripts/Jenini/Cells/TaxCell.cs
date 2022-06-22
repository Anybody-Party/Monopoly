using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxCell : BaseCell
{
    [SerializeField] private float _minTax;
    [SerializeField] private float _maxTax;
    
    public override void OnCharacterEnteredCell(Character character)
    {
        
    }
}