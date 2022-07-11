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
    private int stackColumns = 1;
    private int itemsInColumn = 20;
    private int stackLimit;

    private int giveMoneyCounter = 0;
    private int takeMoneyCounter = 0;

    private Character characterAtCell;
    public bool isRunning;

    private void Awake()
    {
        CreateStackGrid();
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
        _item.transform.SetParent(stackingParent);
        _item.transform.DOScale(Vector3.one, 0.1f).ChangeStartValue(Vector3.zero).SetEase(Ease.OutCubic);
        _item.transform.DOLocalRotate(Vector3.zero, 0.1f).SetEase(Ease.OutCubic);
        _item.transform.DOLocalMove(stackGrid[GetItemsCount()].localPosition, 0.1f).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            _item.transform.DOPunchScale(Vector3.one * 1.2f, 0.1f);
            _item.transform.SetParent(stackGrid[GetItemsCount()], true);
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
        });
    }

    internal void DeleteAll()
    {
        for (int i = 0; i < GetItemsCount(); i++)
            MoneyPool.Instance.PoolizeItem(stackedItems[i]);
    }

    private void TryGiveMoneyToCharacter(int _howMuch)
    {
        giveMoneyCounter = _howMuch;
        isRunning = true;
        StartCoroutine(GiveMoneyToCharacter());
    }

    private IEnumerator GiveMoneyToCharacter()
    {
        yield return new WaitForSeconds(0.1f);
        if (!IsCanGive()) yield break;
        StackableItem lastItem = GetLastItem();
        RemoveLastItem();
        if (!lastItem) yield break;
        lastItem.transform.parent = null;
        lastItem.PickUp(characterAtCell.moneyStack);

        giveMoneyCounter--;
        if (giveMoneyCounter > 0)
            StartCoroutine(GiveMoneyToCharacter());
        else
            isRunning = false;
    }

    private void TryTakeMoneyFromCharacter(int _howMuch)
    {
        takeMoneyCounter = _howMuch;
        StartCoroutine(TakeMoneyFromCharacter());
    }

    private IEnumerator TakeMoneyFromCharacter()
    {
        yield return new WaitForSeconds(0.1f);
        if (!IsCanTake()) yield break;
        var _lastItem = characterAtCell.moneyStack.GetLastItem();
        characterAtCell.moneyStack.RemoveLastItem();
        if (!_lastItem) yield break;
        _lastItem.transform.parent = null;
        _lastItem.PickUp(this);

        takeMoneyCounter--;
        if (takeMoneyCounter > 0)
            StartCoroutine(TakeMoneyFromCharacter());
    }

    private bool IsCanGive()
    {
        return characterAtCell.moneyStack.HasEmptySpace() && GetItemsCount() + 1 > 0;
    }

    private bool IsCanTake()
    {
        return characterAtCell.moneyStack.GetItemsCount() + 1 > 0;
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