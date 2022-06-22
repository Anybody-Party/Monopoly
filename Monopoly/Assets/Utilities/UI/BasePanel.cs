using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BasePanel : UIElement
{
    protected bool panelIsOpen;

    [HideInInspector] public UnityEvent OnHidePanel;
    [HideInInspector] public UnityEvent OnShowPanel;

    protected abstract void Init();

    private void Start()
    {
        Init();
    }

    protected override void SetShow(bool _isShow) // [System.Runtime.CompilerServices.CallerMemberName] string memberName = "" - WHO
    {
        base.SetShow(_isShow);
        if (_isShow)
        {
            OnShowPanel.Invoke();
            panelIsOpen = true;
        }
        else
        {
            OnHidePanel.Invoke();
            panelIsOpen = false;
        }
    }
}
