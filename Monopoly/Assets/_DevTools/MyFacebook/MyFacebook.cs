using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFacebook : MonoBehaviour
{
    #region Singleton Init
    private static MyFacebook _instance;
    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }
    public static MyFacebook Instance // Init not in order
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
        _instance = FindObjectOfType<MyFacebook>();
        _instance.Initialize();
    }
    #endregion

    private void Initialize()
    {
        if (FB.IsInitialized)
            FB.ActivateApp();
        else
            FB.Init(() =>
            {
                FB.ActivateApp();
            });
    }

    public void LogEvent(string _event, Dictionary<string, object> _params)
    {
        if (FB.IsInitialized)
            FB.LogAppEvent(_event, null, _params);
    }

    public void LogEvent(string _event)
    {
        if (FB.IsInitialized)
            FB.LogAppEvent(_event);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            if (FB.IsInitialized)
                FB.ActivateApp();
            else
                FB.Init(() => FB.ActivateApp());
        }
    }
}