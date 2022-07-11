using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public bool isDebug;

    public Level currentLevel;
    public int currentIndex;
    [Header("Dev")]
    public int alwaysLoadLevelId = -1;

    private int lastLevelIndex = 0;

    protected override void Initialize() //asdsad
    {
        GlobalEvents.OnLevelFailed.AddListener(LevelFailed);
        GlobalEvents.OnLevelComplete.AddListener(LevelComplete);

        UIEvents.LevelCompleteGetRewardButtonTap.AddListener(CreateLevel);
        UIEvents.CloseLevelCompletePanelButtonTap.AddListener(CreateLevel);
        UIEvents.RestartButtonTap.AddListener(RestartLevel);
    }

    private void LevelComplete()
    {
        AnalyticManager.Instance.LogEvent_OnLevelFinish();
        DataManager.Instance.mainData.LevelNumber++;
        UIEvents.LevelCompletePanelShow?.Invoke(true);
        //GlobalEvents.OnLevelComplete?.Invoke(DataManager.Instance.mainData.LevelNumber);
    }

    private void LevelFailed()
    {
        AnalyticManager.Instance.LogEvent_OnLevelFailed();
        UIEvents.LevelFailedPanelShow?.Invoke(true);
    }

    private void RestartLevel()
    {
        CreateLevel(DataManager.Instance.mainData.LevelNumber, true);
        AnalyticManager.Instance.LogEvent_OnLevelStart();
        GlobalEvents.OnLevelStart?.Invoke(DataManager.Instance.mainData.LevelNumber);
    }

    private void Start()
    {
        CoroutineActions.ExecuteAction(0.1f, () => { CreateLevel(); });
    }

    [NaughtyAttributes.Button]
    public void CreateLevel()
    {
        CreateLevel(DataManager.Instance.mainData.LevelNumber);
        AnalyticManager.Instance.LogEvent_OnLevelStart();
        GlobalEvents.OnLevelStart?.Invoke(DataManager.Instance.mainData.LevelNumber);
        //Time.timeScale = 1.0f + ((int)(DataManager.Instance.mainData.LevelNumber / 5) * 0.15f);
    }

    public void CreateLevel(int index, bool _isRestart = false)
    {
        currentIndex = _isRestart ? lastLevelIndex : index;

        if (currentIndex > EditorDatabase.Instance.levelDatas.Count - 1)
        {
            currentIndex = Random.Range(0, EditorDatabase.Instance.levelDatas.Count);
            if (EditorDatabase.Instance.levelDatas.Count > 1)
                while (currentIndex == lastLevelIndex)
                {
                    Debug.Log($"Start {currentIndex}");
                    currentIndex = Random.Range(0, EditorDatabase.Instance.levelDatas.Count);
                    Debug.Log($"Roll {currentIndex}");
                }
        }

        if (alwaysLoadLevelId != -1)
        {
            Debug.LogWarning($"<color=yellow>Caution, alwaysLoadLevelId is not -1, so it will load always the same level! levelId: {alwaysLoadLevelId}</color>");
            currentIndex = alwaysLoadLevelId;
        }

        if (currentLevel != null)
            DestroyCurrentLevel();

        CoroutineActions.ExecuteAction(0.1f, () =>
        {
            if (EditorDatabase.Instance.levelDatas.Count == 0)
                Debug.LogError("You forgot fill level data?");

            if (isDebug) Debug.Log($"CreateLevel try with id {index} selected id : {currentIndex} max lvl: { EditorDatabase.Instance.levelDatas.Count - 1}");

            if (EditorDatabase.Instance.levelDatas == null || EditorDatabase.Instance.levelDatas.Count < 1)
            {
                Debug.LogError("levelDatas is empty!");
                return;
            }
            else if (EditorDatabase.Instance.levelDatas.Count <= currentIndex)
            {
                Debug.LogError($"levelDatas is out of range db:{EditorDatabase.Instance.levelDatas.Count}, id:{currentIndex}");
                return;
            }
            else if (EditorDatabase.Instance.levelDatas[currentIndex] == null)
            {
                Debug.LogError($"levelDatas at index {currentIndex} is null");
                return;
            }

            lastLevelIndex = currentIndex;
            GameObject _level = Instantiate(EditorDatabase.Instance.levelDatas[currentIndex], null);
            currentLevel = _level.GetComponent<Level>();
        });
    }

    private bool IsLevelValidLogs(int levelId)
    {
        if (isDebug) Debug.Log($"IsLevelValidLogs {levelId}");

        if (EditorDatabase.Instance.levelDatas[levelId] == null)
        {
            Debug.LogError($"levelDatas at index {currentIndex} is null");
            return false;
        }
        return true;
    }

    [NaughtyAttributes.Button]
    public void DestroyCurrentLevel()
    {
        if (isDebug) Debug.Log($"DestroyCurrentLevel {currentLevel.gameObject}");
        Destroy(currentLevel.gameObject);
    }

    [NaughtyAttributes.Button]
    private void CheckAllLevels()
    {
        if (EditorDatabase.Instance.levelDatas == null || EditorDatabase.Instance.levelDatas.Count < 1)
        {
            Debug.LogError("levelDatas is empty!");
            return;
        }
        else
        {
            for (int i = 0; i < EditorDatabase.Instance.levelDatas.Count; i++)
            {
                IsLevelValidLogs(i);
            }
        }
    }


}
