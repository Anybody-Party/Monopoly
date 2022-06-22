using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class GlobalEvents
{
    //Money
    public static UnityEvent<int, int> AddMoney = new UnityEvent<int, int>(); // playerNum, amount
    public static UnityEvent<int, int> MoneyAdded = new UnityEvent<int, int>();

    //LevelAccuracy
    public static UnityEvent<int> AddLevelAccuracy = new UnityEvent<int>();
    public static UnityEvent<int> LevelAccuracyAdded = new UnityEvent<int>();

    //Camera
    public static UnityEvent MoveCameraToExamplePos = new UnityEvent();

    //Settings
    public static UnityEvent<bool> SetVibrationState = new UnityEvent<bool>();

    public static UnityEvent<int> OnLevelStart = new UnityEvent<int>();
    public static UnityEvent<int> OnLevelComplete = new UnityEvent<int>();
    public static UnityEvent OnLevelFailed = new UnityEvent();
}
