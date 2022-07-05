using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board>
{
    [SerializeField] private BaseCell[] _cells;

    protected override void Initialize()
    {
        InitAllCells();
    }

    public BaseCell GetCellBySteps(BaseCell currentCell, int steps)
    {
        if (steps > 0 && currentCell.GetForwardCell(steps) == null)
            return _cells[0];

        return steps > 0 ? currentCell.GetForwardCell(steps) : currentCell.GetBackCell(steps);
    }

    private int ClampIndex(int index)
    {
        return Mathf.Clamp(index, 0, _cells.Length);
    }

    [ContextMenu("Init")]
    private void InitAllCells()
    {
        _cells = GetComponentsInChildren<BaseCell>();
        for (var i = 0; i < _cells.Length; i++)
            if (i > 0 && i < _cells.Length - 1)
                _cells[i].Init(_cells[i - 1], _cells[i + 1]);
            else if (i > 0)
                _cells[i].Init(_cells[i - 1], null);
            else
                _cells[i].Init(null, _cells[i + 1]);
    }
}