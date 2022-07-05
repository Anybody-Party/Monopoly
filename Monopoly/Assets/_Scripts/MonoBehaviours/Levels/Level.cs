using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private ParticleSystem levelCompletePS;
    [SerializeField] private ParticleSystem levelFailPS;
    [HideInInspector] public UnityEvent OnLevelComplete;
    [HideInInspector] public UnityEvent OnLevelFailed;

    private void Start()
    {
        UIEvents.UpdateLevelProgressBar?.Invoke(0.0f);
        UIEvents.ChangeLevelText?.Invoke($"LEVEL {DataManager.Instance.mainData.LevelNumber + 1}");

        for (int i = 0; i < DataManager.Instance.mainData.CharactersNum; i++)
        {
            GlobalEvents.AddMoney.Invoke(i, DataManager.Instance.balanceData.StartMoney);
            //DataManager.Instance.levelData.SetCurrentCellForCharacter(i, startCell);
        }
    }

    private void LevelComplete()
    {
        CoroutineActions.ExecuteAction(1.5f, () =>
        {
            OnLevelComplete.Invoke();
            levelCompletePS.Play();
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Success);
        });
        CoroutineActions.ExecuteAction(3.0f, () =>
        {
            GlobalEvents.OnLevelComplete?.Invoke(DataManager.Instance.mainData.LevelNumber);
        });
    }

    private void LevelFailed()
    {
        CoroutineActions.ExecuteAction(1.5f, () =>
        {
            OnLevelFailed.Invoke();
            levelFailPS.Play(); MoreMountains.
            NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Failure);
        });
        CoroutineActions.ExecuteAction(3.0f, () =>
        {
            GlobalEvents.OnLevelFailed?.Invoke();
        });
    }
}
