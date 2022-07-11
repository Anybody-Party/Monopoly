using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    private Character character;

    private void Start()
    {
        character = GetComponent<Character>();
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        float _waitSec = 8.0f - DataManager.Instance.mainData.LevelNumber * 0.2f;
        if (_waitSec < 5)
            _waitSec = 5.0f;
        yield return new WaitForSeconds(_waitSec);
        character.TryRollingDices();
        CoroutineActions.WaitForConditionAndDoAction(() => character.isCanRoll, () => { MakeDecision(); });
    }

    private void MakeDecision()
    {
        bool _isBuy = false;
        int _money = DataManager.Instance.levelData.GetCharacterMoney(character.characterNum);

        BuyableCell _buyableCell = DataManager.Instance.levelData.GetCurrentCellForCharacter(character).GetComponent<BuyableCell>();
        _isBuy = _buyableCell != null && _money > _buyableCell.Cost * 2 && Random.Range(0, 1.0f) > 0.3f;

        if (_isBuy)
            _buyableCell.SellOut();

        StartCoroutine(Moving());
    }
}
