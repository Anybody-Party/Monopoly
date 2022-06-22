using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TileConsumer : MonoBehaviour
{
    //[HideInInspector] public UnityEvent AllMedalsGetted = new UnityEvent();

    //private Action<int> MedalGained { get; set; }
    //private float consumeTimer;
    //private int tileNum;

    //private void Start()
    //{
    //    tileNum = GetComponent<Tile>().tileNum;
    //    MedalGained?.Invoke(GameData.Instance.mainData.NeededMedalsToOpenTile[tileNum]);
    //}

    //private void Update()
    //{
    //    if (consumeTimer > 0)
    //        consumeTimer -= Time.deltaTime;
    //}

    //private void Complete()
    //{
    //    AllMedalsGetted?.Invoke();
    //    MedalGained?.Invoke(-1);
    //}

    //public bool IsPlayerCan()
    //{
    //    return GameData.Instance.mainData.NeededMedalsToOpenTile[tileNum] > 0 && consumeTimer <= 0 && GameData.Instance.mainData.Medals > 0;
    //}

    //private bool IsComplete()
    //{
    //    return GameData.Instance.mainData.NeededMedalsToOpenTile[tileNum] <= 0;
    //}

    //public void ConsumeItem(float _time)
    //{
    //    if (consumeTimer <= 0)
    //    {
    //        consumeTimer = _time;
    //        GameData.Instance.mainData.NeededMedalsToOpenTile[tileNum] -= 1;
    //        GlobalEvents.AddMedals.Invoke(-1);
    //        MedalGained?.Invoke(GameData.Instance.mainData.NeededMedalsToOpenTile[tileNum]);
    //        if (IsComplete())
    //            Complete();
    //    }
    //}
}
