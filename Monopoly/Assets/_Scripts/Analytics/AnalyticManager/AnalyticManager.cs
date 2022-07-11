//using AppsFlyerSDK;
//using Firebase.Analytics;
//using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticManager : MonoBehaviour
{
    #region Singleton Init
    private static AnalyticManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static AnalyticManager Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<AnalyticManager>();
        _instance.Initialize();
    }
    #endregion

    private void Initialize()
    {
        //GameAnalytics.Initialize();
    }

    public void LogEvent(string _event)
    {
        MyFacebook.Instance.LogEvent(_event);
        //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Complete, _event);
        //FirebaseAnalytics.LogEvent(_event, "level", Stats.getInstanse.lvl);
        //AppsFlyer.sendEvent(_event, _params);
        //MyTenjin.Instance.instance.SendEvent(_event);
    }

    public void LogEvent_OnLevelStart()
    {
        Dictionary<string, object> _params = new Dictionary<string, object>();
        _params.Add("level_number", DataManager.Instance.mainData.LevelNumber);
        _params.Add("level_name", $"0{DataManager.Instance.mainData.LevelNumber}_name");
        _params.Add("level_count", DataManager.Instance.mainData.LevelNumber);
        _params.Add("level_diff", "easy");
        _params.Add("level_loop", 1);
        _params.Add("level_random", 0);
        _params.Add("level_type", "normal");
        _params.Add("game_mode", "classic");

        MyFacebook.Instance.LogEvent("level_start", _params);
        AppMetrica.Instance.ReportEvent("level_start", _params);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void LogEvent_OnLevelFinish()
    {
        Dictionary<string, object> _params = new Dictionary<string, object>();
        _params.Add("level_number", DataManager.Instance.mainData.LevelNumber);
        _params.Add("level_name", $"0{DataManager.Instance.mainData.LevelNumber}_name");
        _params.Add("level_count", DataManager.Instance.mainData.LevelNumber);
        _params.Add("level_diff", "easy");
        _params.Add("level_loop", 1);
        _params.Add("level_random", 0);
        _params.Add("level_type", "normal");
        _params.Add("game_mode", "classic");
        _params.Add("result", "win");
        _params.Add("time", DataManager.Instance.levelData.LevelTimer);
        _params.Add("progress", "100");
        _params.Add("continue", "0");

        MyFacebook.Instance.LogEvent("level_finish", _params);
        AppMetrica.Instance.ReportEvent("level_finish", _params);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void LogEvent_OnLevelFailed()
    {
        Dictionary<string, object> _params = new Dictionary<string, object>();
        _params.Add("level_number", DataManager.Instance.mainData.LevelNumber);
        _params.Add("level_name", $"0{DataManager.Instance.mainData.LevelNumber}_name");
        _params.Add("level_count", DataManager.Instance.mainData.LevelNumber);
        _params.Add("level_diff", "easy");
        _params.Add("level_loop", 1);
        _params.Add("level_random", 0);
        _params.Add("level_type", "normal");
        _params.Add("game_mode", "classic");
        _params.Add("result", "lose");
        _params.Add("time", DataManager.Instance.levelData.LevelTimer);
        _params.Add("progress", "100");
        _params.Add("continue", "0");

        MyFacebook.Instance.LogEvent("level_finish", _params);
        AppMetrica.Instance.ReportEvent("level_finish", _params);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void LogEvent(string _event, Dictionary<string, object> _params)
    {
        MyFacebook.Instance.LogEvent(_event, _params);
        //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Complete, _event, _param, _value);
        //FirebaseAnalytics.LogEvent(_event, "level", Stats.getInstanse.lvl);
        //FirebaseAnalytics.LogEvent(_event, _param, _value);
        //AppsFlyer.sendEvent(_event, _params);
        //MyTenjin.Instance.instance.SendEvent(_event, _param);
    }
}