using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class GlobalEvents
{
    //Money
    public static UnityEvent<int, int> AddMoney = new UnityEvent<int, int>(); // charNum, amount
    public static UnityEvent<int, int> MoneyAdded = new UnityEvent<int, int>();

    //Camera
    public static UnityEvent MoveCameraToExamplePos = new UnityEvent();

    //Settings
    public static UnityEvent<bool> SetVibrationState = new UnityEvent<bool>();

    //Level
    public static UnityEvent<int> OnLevelStart = new UnityEvent<int>();
    public static UnityEvent<int> OnLevelComplete = new UnityEvent<int>();
    public static UnityEvent OnLevelFailed = new UnityEvent();

    //Cells
    public static UnityEvent<BuyableCell> BuyCell = new UnityEvent<BuyableCell>();
    public static UnityEvent<int> CharacterGameOver = new UnityEvent<int>(); // charNum

    //CharactersSetup
    public static UnityEvent<int, int> SetupCharacters = new UnityEvent<int, int>(); // numbers of characters, start money

}
