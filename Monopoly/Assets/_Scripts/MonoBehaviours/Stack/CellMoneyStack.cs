using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CellMoneyStack : MonoBehaviour
{
    [SerializeField] private Transform stackingParent;
    [SerializeField] private GameObject stackGridPrefab;

    private List<StackableItem> stackedItems = new List<StackableItem>();
    private List<Transform> stackGrid = new List<Transform>();

    private int stackRows = 3;
    private int stackColumns = 3;
    private int itemsInColumn = 15;
    private int stackLimit;

    private float pickUpTimer = 0.3f;
    private float pickUpTime = 0.3f;
    private float resetTimer;

    private Character characterAtCell;

    private void Start()
    {
        CoroutineActions.ExecuteAction(1.5f, () =>
        {
            CreateStackGrid();
            //for (int i = 0; i < DataManager.Instance.mainData.TeamGeneratedMedals; i++)
            //{
            //    StackableItem item = MedalsPool.Instance.UseItem(transform.position);
            //    item.TeammateMedalGenerate(this);
            //}
        });

    }

    private void CreateStackGrid()
    {
        Vector3 _parentPos = new Vector3();
        _parentPos = stackingParent.position;
        for (int z = 0; z < stackRows; z++)
        {
            for (int x = 0; x < stackColumns; x++)
            {
                for (int y = 0; y < itemsInColumn; y++)
                {
                    Vector3 _pos = _parentPos + new Vector3(x * DataManager.Instance.settingsData.StackOffsetX, y * DataManager.Instance.settingsData.StackOffsetY, z * DataManager.Instance.settingsData.StackOffsetZ);
                    stackGrid.Add(Instantiate(stackGridPrefab, _pos, Quaternion.identity, stackingParent).transform);
                }
            }
        }
        stackLimit = stackRows * stackColumns * itemsInColumn;
    }

    public void AddItem(StackableItem _item)
    {
        stackedItems.Add(_item);
        pickUpTimer -= 0.02f;
        if (pickUpTimer <= 0.02f)
            pickUpTimer = 0.02f;
        resetTimer = 1.0f;
        _item.transform.SetParent(stackingParent);
        _item.transform.DOScale(new Vector3(0.2f, 0.02f, 0.2f), pickUpTimer).ChangeStartValue(Vector3.zero).SetEase(Ease.OutCubic);
        _item.transform.DOLocalMove(stackGrid[GetItemsCount()].localPosition, pickUpTimer).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            _item.transform.DOPunchScale(new Vector3(0.2f, 0.02f, 0.2f), 0.1f);
            _item.transform.SetParent(stackGrid[GetItemsCount()], true);
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
        });
    }

    internal void DeleteAll()
    {
        for (int i = 0; i < GetItemsCount(); i++)
            MoneyPool.Instance.PoolizeItem(stackedItems[i]);
    }

    private void Update()
    {
        if (resetTimer > 0)
        {
            if (resetTimer <= 0)
                pickUpTimer = pickUpTime;
            resetTimer -= Time.deltaTime;
        }
    }

    private void TryGiveMoneyToCharacter(int _howMuch)
    {
        for (int i = 0; i < _howMuch; i++)
        {
            if (!IsCanGive()) return;
            //GlobalEvents.AddTeamGeneratedMedals.Invoke(-1);
            StackableItem lastItem = GetLastItem();
            RemoveLastItem();
            if (!lastItem) return;
            lastItem.transform.parent = null;
            lastItem.PickUp(characterAtCell.moneyStack);
            // GlobalEvents.AddMoney.Invoke(1);
        }
    }

    private void TryTakeMoneyFromCharacter(int _howMuch)
    {
        for (int i = 0; i < _howMuch; i++)
        {
            if (!IsCanTake()) return;
            var _lastItem = characterAtCell.moneyStack.GetLastItem();
            characterAtCell.moneyStack.RemoveLastItem();
            if (!_lastItem) return;
            _lastItem.transform.parent = null;
            _lastItem.PickUp(this);
        }
    }

    private bool IsCanGive()
    {
        return characterAtCell.moneyStack.HasEmptySpace();
    }

    private bool IsCanTake()
    {
        return characterAtCell.moneyStack.GetItemsCount() > 0;
    }

    public void GiveMoneyToCharacterAtCell(Character _character, int _howMuch)
    {
        characterAtCell = _character;
        TryGiveMoneyToCharacter(_howMuch);

    }

    public void TakeMoneyFromCharacterAtCell(Character _character, int _howMuch)
    {
        characterAtCell = _character;
        TryTakeMoneyFromCharacter(_howMuch);
    }

    public StackableItem GetLastItem()
    {
        return stackedItems[GetItemsCount()];
    }

    private int GetItemsCount()
    {
        return stackedItems.Count - 1;
    }

    public void ShowItems(bool _isShow)
    {
        if (_isShow)
            foreach (var item in stackedItems)
                item.gameObject.SetActive(false);
        else
            foreach (var item in stackedItems)
                item.gameObject.SetActive(true);
    }

    public void RemoveLastItem()
    {
        if (stackedItems.Count > 0 && GetLastItem())
            stackedItems.Remove(GetLastItem());
    }

    public bool HasEmptySpace()
    {
        return stackedItems.Count < stackLimit;
    }
}