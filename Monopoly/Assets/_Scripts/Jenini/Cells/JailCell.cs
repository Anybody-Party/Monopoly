using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JailCell : BaseCell
{
    [Header("Text")]
    [SerializeField] private Animator secTextAnimator;
    [SerializeField] private TextMeshPro secText;

    private int secCounter;

    public override void OnCharacterEnteredCell(Character character)
    {
        base.OnCharacterEnteredCell(character);
        character.JailWaitBeforeRoll(DataManager.Instance.balanceData.JailStopTime);
        secCounter = 0;
        secText.text = $"{DataManager.Instance.balanceData.JailStopTime}";
        secTextAnimator.SetTrigger("IsShowLong");
        StartCoroutine(SecTimer());
    }

    public override void OnCharacterCrossCell(Character character)
    {
    }

    public override void OnCharacterGoFromCell(Character character)
    {
        base.OnCharacterGoFromCell(character);
    }

    private IEnumerator SecTimer()
    {
        yield return new WaitForSeconds(1.0f);
        secCounter++;
        if (secCounter != 0)
            secText.text = $"{DataManager.Instance.balanceData.JailStopTime - secCounter}";
        else
            secText.text = $"GO!";
        if (secCounter < DataManager.Instance.balanceData.JailStopTime)
            StartCoroutine(SecTimer());
    }
}