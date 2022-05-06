using Coroutines;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    [SerializeField] private Text _textValue;
    [SerializeField] private float _valuesPerSecondSpeed = 1f;
    private int _currentValue;
    private readonly int[] _values = new int[] { 1, 2, 3, 4, 5, 6 };
    private CoroutineObject _rollingRoutine;

    public int CurrentValue => _currentValue;

    private void Awake()
    {
        _rollingRoutine = new CoroutineObject(this, RollingRoutine);
        StartRoll();
    }

    public void StartRoll()
    {
        _rollingRoutine.Start();
    }

    public void StopRoll()
    {
        _rollingRoutine.Stop();
    }

    private IEnumerator RollingRoutine()
    {
        var delay = new WaitForSeconds(1f / _valuesPerSecondSpeed);
        while (true)
        {
            _currentValue = GetRandomValueExceptCurrent();
            _textValue.text = _currentValue.ToString();
            yield return delay;
        }
    }

    private int GetRandomValueExceptCurrent()
    {
        int newValue;
        do
        {
            var randomIndex = Random.Range(0, _values.Length);
            newValue = _values[randomIndex];
        } while (newValue == _currentValue);
        return newValue;
    }
}
