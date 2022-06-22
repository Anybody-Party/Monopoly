using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class BaseText : UIElement
{
    protected TextMeshProUGUI _text { get; private set; }

    protected abstract void Init();

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Init();
    }

    protected void UpdateText(string _value) 
    {
        _text.text = _value;
    }
}
