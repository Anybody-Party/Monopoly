using TMPro;
using UnityEngine;

public class MoneyPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        //UpdateMoneyText(MainData.Instance.Money);
        //MainData.Instance.MoneyChanged += OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        UpdateMoneyText(money);
    }

    private void UpdateMoneyText(int money)
    {
        _text.text = money.ToString();
    }
}