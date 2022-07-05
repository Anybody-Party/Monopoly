using RH.Utilities.Coroutines;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel : BasePanel
{
    [SerializeField] private List<TextMeshProUGUI> leaderboardTexts;

    protected override void Init()
    {
        UIEvents.LeaderboardPanelShow.AddListener(SetShow);
        GlobalEvents.MoneyAdded.AddListener(UpdateInfo);
    }

    private void UpdateInfo(int _characterNum, int _money)
    {
        leaderboardTexts[_characterNum].text = $"{DataManager.Instance.levelData.GetCharacterMoney(_characterNum)}$";
    }
}
