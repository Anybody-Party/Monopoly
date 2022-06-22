using UnityEngine;
using UnityEngine.Events;


public static class InputEvents
{
    public static UnityEvent<Vector2> FingerTap = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> FingerDown = new UnityEvent<Vector2>();
    public static UnityEvent<Vector2> FingerUp = new UnityEvent<Vector2>();

}
