using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] public MainData mainData;
    [SerializeField] public BalanceData balanceData;
    [SerializeField] public SettingsData settingsData;
    [SerializeField] public LevelData levelData;

    protected override void Initialize()
    {
        Debug.Log(Utility.GetDataPath());
        LoadData();
    }

    private void SaveData()
    {
        mainData.SaveData();
        settingsData.SaveData();
    }

    private void LoadData()
    {
        mainData.LoadData();
        settingsData.LoadData();
    }

    [NaughtyAttributes.Button]
    public void ResetData()
    {
        mainData.ResetData();
        levelData.ResetData();
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
    [MenuItem("Tools/DeleteAllGameData")]
    public static void DeleteAllGameData()
    {
        if (Directory.Exists(Utility.GetDataPath()))
            Directory.Delete(Utility.GetDataPath(), true);
    }
#endif

    private void OnApplicationQuit()
    {
        SaveData();
        DeleteGameData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveData();
    }

    private void DeleteGameData()
    {
#if UNITY_EDITOR
        ResetData();
        DeleteAllGameData(); // TODO: REMOVE
#endif
    }
}

