using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItem : MonoBehaviour
{
    //public ParticleSystem shinePS;

    public void PickUp(MoneyStack _stack)
    {
        if (!_stack.HasEmptySpace()) return;

        transform.parent = null;
        //shinePS.Play();
        _stack.AddItem(this.GetComponent<StackableItem>());
    }

    public void PickUp(CellMoneyStack _stack)
    {
        if (!_stack.HasEmptySpace()) return;

        transform.parent = null;
        //shinePS.Play();
        _stack.AddItem(this.GetComponent<StackableItem>());
    }

    public void Poolize(Vector3 _pos, float _time)
    {
        // previousStacked = null;
        transform.DOMove(_pos, _time).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            MoneyPool.Instance.PoolizeItem(this);
        });
        transform.DOScale(Vector3.zero, _time).SetEase(Ease.InBack);
    }
}
