using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeMoneyText.AddListener(UpdateText);
    }
}
