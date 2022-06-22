using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MainData", menuName = "GameData/MainData")]
public class MainData : BaseData
{
    public int LevelNumber;
    public List<int> Money;
    public int RealPlayerNum;

    public bool TutorialTapToFixCompleted;
    public bool TutorialTapOnTimeCompleted;

    public override void ResetData()
    {
        
    }
}