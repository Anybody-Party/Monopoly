using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BalanceData", menuName = "GameData/BalanceData")]
public class BalanceData : BaseData
{
    public int MoneyMaxCap;
    public int BaseRewardForLevel;
    public int StartMoney;
    public float RollingTime;
    public int RewardForCompleteLoop;
    public int JailStopTime;
    public int Tax;
    public int RewardForRewardCell;
    public int GoBackSteps;
    public float JumpToTileTime;

    public override void ResetData()
    {
        throw new System.NotImplementedException();
    }
}
