using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "GameData/SettingsData")]
public class SettingsData : BaseData
{
    public bool IsVibrationOn;
    public int MoneyPoolSize;

    public float StackOffsetX;
    public float StackOffsetY;
    public float StackOffsetZ;

    public override void ResetData()
    {
        throw new System.NotImplementedException();
    }
}
