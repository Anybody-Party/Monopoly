using RH.Utilities.ComponentSystem;
using RH.Utilities.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimerSystem : BaseSystem
{
    public override void Dispose()
    {
    }

    protected override void Init()
    {
        CoroutineLauncher.Start(LevelTimerUpdate());
    }

    private IEnumerator LevelTimerUpdate()
    {
        yield return new WaitForSeconds(1.0f);
        DataManager.Instance.levelData.LevelTimer += 1;
        CoroutineLauncher.Start(LevelTimerUpdate());
    }
}
