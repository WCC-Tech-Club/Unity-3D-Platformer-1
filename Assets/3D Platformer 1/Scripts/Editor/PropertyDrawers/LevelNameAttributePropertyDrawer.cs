using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LevelNameAttribute))]
public sealed class LevelNameAttributePropertyDrawer : PropertyDrawer
{
    private const float HelpBoxHeightModifier = 2.5f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float baseHeight = base.GetPropertyHeight(property, label);

        if (property.propertyType == SerializedPropertyType.String && !LevelNameUtility.IsInBuildSettings(property.stringValue))
        {
            baseHeight += EditorGUIUtility.singleLineHeight * HelpBoxHeightModifier;
        }

        return baseHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect stringPosition = new Rect(position);

        if (property.propertyType == SerializedPropertyType.String && !LevelNameUtility.IsInBuildSettings(property.stringValue))
        {
            Rect helpBoxPosition = new Rect(position);
            helpBoxPosition.height = EditorGUIUtility.singleLineHeight * HelpBoxHeightModifier;

            stringPosition.height = position.height - helpBoxPosition.height;
            stringPosition.y += helpBoxPosition.height;

            EditorGUI.HelpBox(helpBoxPosition, "Level `" + property.stringValue + "` is not found in the build settings!", MessageType.Warning);
        }

        EditorGUI.PropertyField(stringPosition, property, label);
    }
}
