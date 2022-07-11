using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPool : Singleton<MoneyPool>
{
    [SerializeField]
    private GameObject moneyPrefab;

    private List<StackableItem> moneyPool = new List<StackableItem>();

    protected override void Initialize()
    {
        for (var i = 0; i < DataManager.Instance.settingsData.MoneyPoolSize; i++)
            CreateMoney();
    }

    private void CreateMoney()
    {
        GameObject _money = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        PoolizeItem(_money.GetComponent<StackableItem>());
    }

    public StackableItem UseItem()
    {
        if (moneyPool.Count == 0)
            CreateMoney();

        StackableItem item = moneyPool[0];
        moneyPool.Remove(item);

        item.gameObject.SetActive(true);
        return item;
    }

    public void PoolizeItem(StackableItem item)
    {
        item.gameObject.SetActive(false);
        item.transform.SetParent(transform, true);

        moneyPool.Add(item);
    }
}
