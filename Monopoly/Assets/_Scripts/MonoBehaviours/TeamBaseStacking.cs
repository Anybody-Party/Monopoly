using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class TeamBaseStacking : MonoBehaviour
{
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] private Transform stackingParent;
    [SerializeField] private GameObject stackGridPrefab;

    private List<StackableItem> stackedItems = new List<StackableItem>();
    private List<Transform> stackGrid = new List<Transform>();

    private bool isPlayerInTrigger;

    private int stackRows = 8;
    private int stackColumns = 3;
    private int itemsInColumn = 15;
    private int stackLimit;

    private float pickUpTimer = 0.3f;
    private float pickUpTime = 0.3f;
    private float resetTimer;

    private void Start()
    {
        playerTrigger.PlayerOnTrigger.AddListener(SetPlayerInTrigger);
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
            _item.shinePS.Stop();
        });
    }

    private void Update()
    {
        if (resetTimer > 0)
        {
            if (resetTimer <= 0)
                pickUpTimer = pickUpTime;
            resetTimer -= Time.deltaTime;
        }

        if (isPlayerInTrigger)
            TryTransmitToPlayer();
    }

    private void TryTransmitToPlayer()
    {
        if (!TeamBaseCan()) return;
        //GlobalEvents.AddTeamGeneratedMedals.Invoke(-1);
        StackableItem lastItem = GetLastItem();
        RemoveLastItem();
        if (!lastItem) return;
        lastItem.transform.parent = null;
        lastItem.PickUp(MoneyStack.Instance);
       // GlobalEvents.AddMoney.Invoke(1);
    }

    public bool TeamBaseCan()
    {
        return true; //PlayerStacking.Instance.HasEmptySpace() && GameData.Instance.mainData.TeamGeneratedMedals > 0;
    }

    private void SetPlayerInTrigger(bool _isPlayerInTrigger)
    {
        isPlayerInTrigger = _isPlayerInTrigger;
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