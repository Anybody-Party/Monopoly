using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "GameData/SettingsData")]
public class SettingsData : BaseData
{
    public bool IsVibrationOn;
    public int MoneyPoolSize;

    public int StackOffsetX;
    public int StackOffsetY;
    public int StackOffsetZ;

    public override void ResetData()
    {
        throw new System.NotImplementedException();
    }
}
