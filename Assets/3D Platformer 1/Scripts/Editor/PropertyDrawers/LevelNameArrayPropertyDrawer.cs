using UnityEngine;
using UnityEditor;

using Rotorz.ReorderableList;

[CustomPropertyDrawer(typeof(LevelNameArray), false)]
public sealed class LevelNameArrayPropertyDrawer : PropertyDrawer
{
    private const float TitleHeightModifier = 1.5f;
    private const float HelpBoxHeightModifier = 2.5f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty stringsProperty = property.FindPropertyRelative("strings");
        bool allValid = true;
        
        for (int i = 0; i < stringsProperty.arraySize; i++)
        {
            if (!LevelNameUtility.IsInBuildSettings(stringsProperty.GetArrayElementAtIndex(i).stringValue))
            {
                allValid = false;
            }
        }

        return EditorGUIUtility.singleLineHeight * TitleHeightModifier +
            (!allValid ? EditorGUIUtility.singleLineHeight * HelpBoxHeightModifier : 0) +
            ReorderableListGUI.CalculateListFieldHeight(stringsProperty);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty stringsProperty = property.FindPropertyRelative("strings");
        bool allValid = true;

        for (int i = 0; i < stringsProperty.arraySize; i++)
        {
            if (!LevelNameUtility.IsInBuildSettings(stringsProperty.GetArrayElementAtIndex(i).stringValue))
            {
                allValid = false;
            }
        }

        ReorderableListStyles.Title.fontStyle = FontStyle.Bold;

        Rect titlePosition = new Rect(position);
        titlePosition.height = EditorGUIUtility.singleLineHeight * TitleHeightModifier;
        ReorderableListGUI.Title(titlePosition, label);

        Rect listPosition = new Rect(position);

        if (!allValid)
        {
            Rect helpBoxPosition = new Rect(position);
            helpBoxPosition.height = EditorGUIUtility.singleLineHeight * HelpBoxHeightModifier;
            helpBoxPosition.y += titlePosition.height;

            listPosition.height = position.height - (titlePosition.height + helpBoxPosition.height);
            listPosition.y += (titlePosition.height + helpBoxPosition.height);
            
            EditorGUI.HelpBox(helpBoxPosition, "Not all levels are found in build settings!", MessageType.Warning);
        }
        else
        {
            listPosition.height = position.height - titlePosition.height;
            listPosition.y += titlePosition.height;
        }

        ReorderableListGUI.ListFieldAbsolute(listPosition, stringsProperty);
    }
}
