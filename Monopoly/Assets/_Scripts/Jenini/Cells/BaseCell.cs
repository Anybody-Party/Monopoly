using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCell : MonoBehaviour
{
    [SerializeField] private List<Transform> _characterPoints;
    [SerializeField] private BaseCell _previousCell;
    [SerializeField] private BaseCell _nextCell;
    [SerializeField] protected Character _characterOnCell;
    
    public Transform GetCharacterPoint(int _characterNum) => _characterPoints[_characterNum];

    public void Init(BaseCell previousCell, BaseCell nextCell)
    {
        _previousCell = previousCell;
        _nextCell = nextCell;
    }

    public BaseCell GetForwardCell(int steps)
    {
        return steps == 1 ? _nextCell : _nextCell.GetForwardCell(steps - 1);
    }

    public BaseCell GetBackCell(int steps)
    {
        return steps == 1 ? _previousCell : _previousCell.GetBackCell(steps - 1);
    }
    
    public virtual void OnCharacterEnteredCell(Character character)
    {
        DataManager.Instance.levelData.SetCurrentCellForCharacter(character, this);
        _characterOnCell = character;
    }

    public abstract void OnCharacterCrossCell(Character character);
}