using UnityEngine;

[CreateAssetMenu(menuName = "Cube Sides")]
public class CubeSides : ScriptableObject
{
    public CubeSide[] Sides;
}

[System.Serializable]
public struct CubeSide
{
    public Vector3 Rotation;
    public int Value;
}