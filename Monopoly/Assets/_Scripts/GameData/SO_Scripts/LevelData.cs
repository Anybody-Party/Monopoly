using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData")]
public class LevelData : BaseData
{
    [SerializeField] private List<int> Moneys;
    [SerializeField] private List<BaseCell> CurrentCells;
    [SerializeField] public int LevelTimer;

    public override void ResetData()
    {
        for (int i = 0; i < Moneys.Count; i++)
            Moneys[i] = 0;
        for (int i = 0; i < CurrentCells.Count; i++)
            CurrentCells[i] = null;

        LevelTimer = 0;
    }

    public void SetCurrentCellForCharacter(Character _character, BaseCell _cell)
    {
        CurrentCells[_character.characterNum] = _cell;
    }

    public BaseCell GetCurrentCellForCharacter(Character _character)
    {
        return CurrentCells[_character.characterNum];
    }

    public void AddMoneyForCharacter(int _characterNum, int _money)
    {
        Moneys[_characterNum] += _money;
    }

    public int GetCharacterMoney(int _characterNum)
    {
        return Moneys[_characterNum];
    }

    public int GetPlayerMoney()
    {
        return Moneys[DataManager.Instance.mainData.RealPlayerNum];
    }

    public void AddPlayerMoney(int _money)
    {
        Moneys[DataManager.Instance.mainData.RealPlayerNum] += _money;
    }
}