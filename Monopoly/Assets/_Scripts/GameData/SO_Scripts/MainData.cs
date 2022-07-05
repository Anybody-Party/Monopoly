using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MainData", menuName = "GameData/MainData")]
public class MainData : BaseData
{
    public int LevelNumber;
    public int RealPlayerNum;
    public int CharactersNum;
    public string[] CharacterCellNames;

    public bool TutorialTapToFixCompleted;
    public bool TutorialTapOnTimeCompleted;

    public override void ResetData()
    {
        
    }
}