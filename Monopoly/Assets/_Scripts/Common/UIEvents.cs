using UnityEngine;
using UnityEngine.Events;


public static class UIEvents
{
    //VibrationButton
    public static UnityEvent VibrationButtonTap = new UnityEvent();
    public static UnityEvent<bool> VibrationButtonShow = new UnityEvent<bool>();

    //MoneyText
    public static UnityEvent<string> ChangeMoneyText = new UnityEvent<string>();

    //LevelText
    public static UnityEvent<string> ChangeLevelText = new UnityEvent<string>();

    //LevelProgressBar
    public static UnityEvent<float> UpdateLevelProgressBar = new UnityEvent<float>();

    //LevelCompletePanelShow
    public static UnityEvent<bool> LevelCompletePanelShow = new UnityEvent<bool>();
    public static UnityEvent<string> ChangeLevelCompleteMoneyText = new UnityEvent<string>();
    public static UnityEvent<string> ChangeLevelAccuracyText = new UnityEvent<string>();
    public static UnityEvent LevelCompleteGetRewardButtonTap = new UnityEvent();
    public static UnityEvent CloseLevelCompletePanelButtonTap = new UnityEvent();
    public static UnityEvent<bool> LevelCompleteGetRewardButtonShow = new UnityEvent<bool>();

    //LevelFailedPanelShow
    public static UnityEvent<bool> LevelFailedPanelShow = new UnityEvent<bool>();
    public static UnityEvent<bool> OpenLevelFailedPanelButtonShow = new UnityEvent<bool>();
    public static UnityEvent RestartButtonTap = new UnityEvent();

    //RollDiceButton
    public static UnityEvent RollDiceButtonTap = new UnityEvent();

    //TapToStart
    public static UnityEvent<bool> TapToStartTextShow = new UnityEvent<bool>();
    public static UnityEvent TapToStartButtonTap = new UnityEvent();

    //Commendable
    public static UnityEvent ShowCommend = new UnityEvent();

    //Tutorial
    public static UnityEvent<bool> TutorialTapToFixTextShow = new UnityEvent<bool>();
    public static UnityEvent<bool> TutorialTapOnTimeTextShow = new UnityEvent<bool>();
}
