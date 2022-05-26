#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Condition))]
public class ConditionPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Condition condition = (Condition)attribute;
        bool enabled = GetHideResult(condition, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if (enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Condition condition = (Condition)attribute;
        bool enabled = GetHideResult(condition, property);

        if (enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }

    private bool GetHideResult(Condition condition, SerializedProperty property)
    {
        bool enabled = true;
        string conditionPath = property.propertyPath.Replace(property.name, condition.SourceField);
        SerializedProperty sourceProperty = property.serializedObject.FindProperty(conditionPath);

        switch (sourceProperty.propertyType)
        {
            case SerializedPropertyType.Boolean:
                enabled = GetHideBoolResult(condition, sourceProperty);
                break;
            case SerializedPropertyType.Enum:
                enabled = GetHideEnumResult(condition, sourceProperty);
                break;
        }

        return enabled;
    }

    private bool GetHideBoolResult(Condition condition, SerializedProperty sourceProperty)
    {
        if (condition.Inverse)
        {
            return !sourceProperty.boolValue;
        }
        else
        {
            return sourceProperty.boolValue;
        }
    }

    private bool GetHideEnumResult(Condition condition, SerializedProperty sourceProperty)
    {
        if (condition.Inverse)
        {
            return Convert.ToInt32(condition.EnumValueIndex) != sourceProperty.enumValueIndex;
        }
        else
        {
            return Convert.ToInt32(condition.EnumValueIndex) == sourceProperty.enumValueIndex;
        }
    }
}
#endif