using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class Condition : PropertyAttribute
{
    public string SourceField;
    public int EnumValueIndex;
    public bool Inverse;

    public Condition(string sourceField, bool inverse = false)
    {
        SourceField = sourceField;
        Inverse = inverse;
    }

    public Condition(string sourceField, int enumValueIndex, bool inverse = false)
    {
        SourceField = sourceField;
        EnumValueIndex = enumValueIndex;
        Inverse = inverse;
    }
}
