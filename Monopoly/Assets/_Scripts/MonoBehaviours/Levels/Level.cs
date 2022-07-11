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

    [SerializeField] private StartCell startCell;
    [SerializeField] private Character[] characters;

    [HideInInspector] public UnityEvent OnLevelComplete;
    [HideInInspector] public UnityEvent OnLevelFailed;

    private int losersCounter = 0;

    private void Start()
    {
        UIEvents.UpdateLevelProgressBar?.Invoke(0.0f);
        UIEvents.ChangeLevelText?.Invoke($"LEVEL {DataManager.Instance.mainData.LevelNumber + 1}");
        UIEvents.CellPanelShow?.Invoke(false, null);
        GlobalEvents.CharacterGameOver.AddListener(CharacterLose);
        DataManager.Instance.levelData.ResetData();
        CameraController.Instance.MoveToAllLevelView();
        CoroutineActions.ExecuteAction(1.5f, () => { CameraController.Instance.MoveToPlayerView(); });
        CoroutineActions.ExecuteAction(1.8f, () =>
        {
            for (int i = 0; i < DataManager.Instance.mainData.CharactersNum; i++)
            {
                characters[i].SetStartCell(startCell);
                startCell.OnCharacterEnteredCell(characters[i]);
                DataManager.Instance.levelData.SetCurrentCellForCharacter(characters[i], startCell);
            }
        });
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
            GlobalEvents.OnLevelComplete?.Invoke();
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

    private void CharacterLose(int _characterNum)
    {
        if (_characterNum == DataManager.Instance.mainData.RealPlayerNum)
            LevelFailed();
        else
        {
            losersCounter++;
            if (losersCounter >= DataManager.Instance.mainData.CharactersNum - 1)
                LevelComplete();
        }
    }
}
