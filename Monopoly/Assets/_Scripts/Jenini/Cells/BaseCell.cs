using UnityEngine;

public abstract class BaseCell : MonoBehaviour
{
    [SerializeField] private Transform _characterPoint;
    [SerializeField] private BaseCell _previousCell;
    [SerializeField] private BaseCell _nextCell;
    
    public Transform CharacterPoint => _characterPoint;

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
    
    public abstract void OnCharacterEnteredCell(Character character);
}