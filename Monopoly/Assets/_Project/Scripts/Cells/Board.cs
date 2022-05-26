using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BaseCell[] _cells;

    public BaseCell GetCellBySteps(BaseCell currentCell, int steps)
    {
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
        {
            if (i > 0 && i < _cells.Length - 1)
            {
                _cells[i].Init(_cells[i - 1], _cells[i + 1]);
            }
            else if (i > 0)
            {
                _cells[i].Init(_cells[i - 1], null);
            }
            else
            {
                _cells[i].Init(null, _cells[i + 1]);
            }
        }
    }
}