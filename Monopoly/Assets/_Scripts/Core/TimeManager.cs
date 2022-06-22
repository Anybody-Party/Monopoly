using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class TimeManager : MonoBehaviour
{
    #region Singleton Init
    private static TimeManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
    }

    public static TimeManager Instance // Init not in order
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
        _instance = FindObjectOfType<TimeManager>();
        if (_instance != null)
            _instance.Initialize();
    }
    #endregion

    [NaughtyAttributes.ShowNativeProperty]
    string CurrentTime => (DateTimeOffset.Now.ToOffset(TimeSpan.Zero).ToString());
    [NaughtyAttributes.ShowNativeProperty]
    string OfflineDateTime => (PlayerPrefs.HasKey(Offline.OfflineTimeKey) ? DTOLoad(Offline.OfflineTimeKey).ToString() : "");
    [NaughtyAttributes.ShowNativeProperty]
    string PicturePaintingProcessTime => (PlayerPrefs.HasKey(PicturePaintingProcess.PicturePaintingProcessTimeKey) ? DTOLoad(PicturePaintingProcess.PicturePaintingProcessTimeKey).ToString() : "");

    public DateTimeOffset? netTime;

    void Initialize()
    {
        StartCoroutine(GetNetTime());

        if (!PlayerPrefs.HasKey("IsFirstLaunch"))
        {
            // Init start values
            PlayerPrefs.SetString("IsFirstLaunch", "no");
            SaveWithCurrentTime(Offline.OfflineTimeKey, new TimeSpan(0, 0, 0));
        }
        // Init data here
        enabled = true;
    }

    public class Offline
    {
        public const string OfflineTimeKey = "CloseTime";

        public static void Hack_Rollback5Min()
        {
            Instance.AddToExist(OfflineTimeKey, new TimeSpan(0, -5, 0));
        }

        public static void SaveQuitTimeForOfflineBonus()
        {
            Instance.SaveWithNetTime(OfflineTimeKey, new TimeSpan(0, 0, 0));
        }

        public static DateTimeOffset LoadOfflineBonus()
        {
            return Instance.DTOLoad(OfflineTimeKey);
        }

        //public static TimeSpan GetCurrentTimeDifference()
        //{
        //    var diff = Instance.GetTimeDifference(OfflineTimeKey);
        //    if (diff.TotalSeconds < 0f)
        //        return new TimeSpan(0, 0, 0);
        //    return diff;
        //}

        //public static bool IsNewRewardBecameAvailable()
        //{
        //    return Instance.IsNetTimeMoreThanSavedValue(OfflineTimeKey);
        //}
    }

    public class PicturePaintingProcess
    {
        public const string PicturePaintingProcessTimeKey = "PicturePaintingProcessTimeKey";
        public const string PicturePaintingOfflineProgressTimeKey = "PicturePaintingOfflineProgressTimeKey";

        public static void Hack_Rollback5Min()
        {
            Instance.AddToExist(PicturePaintingProcessTimeKey, new TimeSpan(0, -5, 0));
        }

        public static void SaveQuitTimeForOfflineProgress()
        {
            Instance.SaveWithNetTime(PicturePaintingOfflineProgressTimeKey, new TimeSpan(0, 0, 0));
        }

        public static void StartNewPicture(TimeSpan _paintingTime)
        {
            Instance.SaveWithNetTime(PicturePaintingProcessTimeKey, _paintingTime);
        }

        public static void AddPaintingTime(TimeSpan _paintingTime)
        {
            Instance.AddToExist(PicturePaintingProcessTimeKey, _paintingTime);
        }

        public static DateTimeOffset LoadOfflineProgress()
        {
            return Instance.DTOLoad(PicturePaintingProcessTimeKey);
        }

        public static TimeSpan GetTimeToComplete()
        {
            var diff = Instance.GetTimeDifference(PicturePaintingProcessTimeKey);
            if (diff.TotalSeconds < 0f)
                return new TimeSpan(0, 0, 0);
            return diff;
        }

        //public static bool IsNewRewardBecameAvailable()
        //{
        //    return Instance.IsNetTimeMoreThanSavedValue(OfflineTimeKey);
        //}
    }

    #region Use it! (High level api)

    void SaveWithCurrentTime(string key)
    {
        PlayerPrefs.SetString(key, ToStrCurrentTime(TimeSpan.Zero));
    }

    void SaveWithCurrentTime(string key, TimeSpan add)
    {
        PlayerPrefs.SetString(key, ToStrCurrentTime(add));
    }

    void SaveWithNetTime(string key, TimeSpan add)
    {
        PlayerPrefs.SetString(key, ToStrCurrentTime(add));
    }

    void AddToExist(string key, TimeSpan add)
    {
        SaveWithSpecifiedValue(key, DTOLoad(key).Add(add));
    }

    bool IsNetTimeMoreThanSavedValue(string key)
    {
        var current = netTime;
        if (netTime != null)
        {
            var saved = DTOLoad(key);
            return (current - saved).Value.TotalHours >= 0f;
        }
        else
            return false;
    }

    bool IsCurrentTimeMoreThanSavedValue(string key)
    {
        var current = DateTimeOffset.Now;
        var saved = DTOLoad(key);
        return (current - saved).TotalHours >= 0f;
    }

    TimeSpan GetTimeDifference(string key, bool isCurrentMore = false)
    {
        if (isCurrentMore)
        {
            var dtoDiff = Instance.ToDTONetTimeAsUnixTime() - Instance.DTOLoad(key);
            return new TimeSpan(dtoDiff.Hours, dtoDiff.Minutes, dtoDiff.Seconds);
        }
        else
        {
            var dtoDiff = Instance.DTOLoad(key) - Instance.ToDTONetTimeAsUnixTime();
            return new TimeSpan(dtoDiff.Hours, dtoDiff.Minutes, dtoDiff.Seconds);
        }
    }

    #endregion

    public void ManualGetNetTime()
    {
        StartCoroutine(GetNetTime());
    }

    public IEnumerator GetNetTime()
    {
        UnityWebRequest myHttpWebRequest = UnityWebRequest.Get("https://www.google.com");
        yield return myHttpWebRequest.SendWebRequest();

        if (myHttpWebRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("NETWORK ERROR");
            netTime = null;
            yield break;
        }
        string netTimeString = myHttpWebRequest.GetResponseHeader("date");
        if (netTimeString != "")
            netTime = DateTimeOffset.ParseExact(netTimeString,
                                           "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                           CultureInfo.InvariantCulture.DateTimeFormat,
                                           DateTimeStyles.AssumeUniversal);
        Debug.Log("Global UTC time: " + netTime);
    }

    private void Update()
    {
        if (netTime != null)
            netTime = netTime.Value.AddSeconds(Time.deltaTime);
    }

    #region Dont use - it's internal (Low level api)

    string StrLoad(string key)
    {
        return PlayerPrefs.GetString(key, "");
    }

    DateTimeOffset DTOLoad(string key)
    {
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString(key, "")))
            return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(PlayerPrefs.GetString(key)));
        //throw new System.NotSupportedException();
        return DateTimeOffset.Now;
    }

    void SaveWithSpecifiedValue(string key, DateTimeOffset dto)
    {
        PlayerPrefs.SetString(key, dto.ToUnixTimeMilliseconds().ToString());
    }

    string ToStrCurrentTime(TimeSpan add)
    {
        //return DateTimeOffset.Now.Add(add).ToUnixTimeMilliseconds().ToString();
        return netTime?.Add(add).ToUnixTimeMilliseconds().ToString();
    }

    //public DateTimeOffset ToDTOCurrentTime()
    //{
    //    return DateTimeOffset.Now;
    //}

    DateTimeOffset ToDTOCurrentTimeAsUnixTime()
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()));
    }

    DateTimeOffset ToDTONetTimeAsUnixTime()
    {
        //return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()));
#if UNITY_EDITOR
        return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()));
#endif
        return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(netTime?.ToUnixTimeMilliseconds().ToString()));
    }

    #endregion

    private void OnApplicationPause(bool _isPause)
    {
        if (!_isPause)
            StartCoroutine(GetNetTime());
    }

}
