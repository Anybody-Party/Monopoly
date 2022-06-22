using RH.Utilities.ComponentSystem;
using RH.Utilities.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : BaseSystem
{
    public override void Dispose()
    {
        GlobalEvents.AddMoney.RemoveListener(AddMoney);
    }

    protected override void Init()
    {
        GlobalEvents.AddMoney.AddListener(AddMoney);
        UIEvents.LevelCompleteGetRewardButtonTap.AddListener(() => { AddMoney(DataManager.Instance.mainData.RealPlayerNum, DataManager.Instance.balanceData.BaseRewardForLevel * 3); });
        UIEvents.CloseLevelCompletePanelButtonTap.AddListener(() => { AddMoney(DataManager.Instance.mainData.RealPlayerNum, DataManager.Instance.balanceData.BaseRewardForLevel); });
    }

    private void AddMoney(int _playerNum, int _amount)
    {
        if (DataManager.Instance.mainData.Money[_playerNum] + _amount < 0) // NotEnough
        {
            return;
        }

        if (_amount > 0 && DataManager.Instance.mainData.Money[_playerNum] + _amount > DataManager.Instance.balanceData.MoneyMaxCap) // Max Cap
        {
            DataManager.Instance.mainData.Money[_playerNum] = DataManager.Instance.balanceData.MoneyMaxCap;
            GlobalEvents.MoneyAdded?.Invoke(_playerNum, _amount);
            return;
        }

        DataManager.Instance.mainData.Money[_playerNum] += _amount;
        GlobalEvents.MoneyAdded?.Invoke(_playerNum, _amount);
    }
}