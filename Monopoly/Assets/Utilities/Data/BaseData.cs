using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class BaseData : ScriptableObject
{
    public abstract void ResetData();

    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        Debug.Log(json);
        string _filename = $"{this.GetType()}.json";
        string _dataPath = Path.Combine(Utility.GetDataPath(), _filename);
        if (!Directory.Exists(Utility.GetDataPath()))
            Directory.CreateDirectory(Utility.GetDataPath());
        File.WriteAllText(_dataPath, json);
    }

    public void LoadData()
    {
        string _filename = $"{this.GetType()}.json";
        string _dataPath = Path.Combine(Utility.GetDataPath(), _filename);
        if (File.Exists(_dataPath))
        {
            string json = File.ReadAllText(_dataPath);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}
