using UnityEditor;
using UnityEngine;

public class EntryPoint : Singleton<EntryPoint>
{
    private void Start()
    {
        IncreaseTargetFrameRate();
        CreateSystems();
    }

    private static void IncreaseTargetFrameRate() =>
        Application.targetFrameRate = 60;

    private static void CreateSystems()
    {
        //GameLogic
        new MoneySystem();
        new LevelTimerSystem();
        new VibrationSystem();
        new LeanTouchSystem();
        new CellBuySystem();

        //Alanytics
        new LevelFinishAnalyticsSender();
        new LevelFailAnalyticsSender();
    }

    protected override void Initialize()
    {
    }
}
