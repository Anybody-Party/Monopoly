using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class MoneyStack : MonoBehaviour
{
    [SerializeField] private bool isRealPlayer = false;
    [SerializeField] private Transform stackingParent;
    [SerializeField] private GameObject stackGridPrefab;
    private List<StackableItem> stackedItems = new List<StackableItem>();
    private List<Transform> stackGrid = new List<Transform>();

    private int stackRows = 2;
    private int stackColumns = 1;
    private int itemsInColumn = 15;
    private int stackLimit;

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
            if (isRealPlayer)
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
        });
    }

    public StackableItem GetLastItem()
    {
        return stackedItems[GetItemsCount()];
    }

    public int GetItemsCount()
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