using UnityEngine;
using UnityEditor;

using Rotorz.ReorderableList;

[CustomPropertyDrawer(typeof(LevelNameArray), false)]
public sealed class LevelNameArrayPropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty stringsProperty = property.FindPropertyRelative("strings");
        bool allValid = true;

        EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
        for (int i = 0; i < stringsProperty.arraySize; i++)
        {
            bool found = false;
            bool empty = false;

            for (int j = 0; j < buildScenes.Length; j++)
            {
                string listValue = stringsProperty.GetArrayElementAtIndex(i).stringValue;
                if (listValue.Length == 0)
                {
                    empty = true;
                    break;
                }
                if (buildScenes[j].path.Contains(stringsProperty.GetArrayElementAtIndex(i).stringValue + ".unity"))
                {
                    found = true;
                    break;
                }
            }

            if (!found || empty)
            {
                allValid = false;
                break;
            }
        }

        return EditorGUIUtility.singleLineHeight * 1.5f +
            (!allValid ? EditorGUIUtility.singleLineHeight * 2 : 0) +
            ReorderableListGUI.CalculateListFieldHeight(stringsProperty);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty stringsProperty = property.FindPropertyRelative("strings");
        bool allValid = true;

        EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
        for (int i = 0; i < stringsProperty.arraySize; i++)
        {
            bool found = false;
            bool empty = false;

            for (int j = 0; j < buildScenes.Length; j++)
            {
                string listValue = stringsProperty.GetArrayElementAtIndex(i).stringValue;
                if (listValue.Length == 0)
                {
                    empty = true;
                    break;
                }
                if (buildScenes[j].path.Contains(stringsProperty.GetArrayElementAtIndex(i).stringValue + ".unity"))
                {
                    found = true;
                    break;
                }
            }

            if (!found || empty)
            {
                allValid = false;
                break;
            }
        }

        ReorderableListStyles.Title.fontStyle = FontStyle.Bold;

        Rect titlePosition = new Rect(position);
        titlePosition.height = EditorGUIUtility.singleLineHeight * 1.5f;
        ReorderableListGUI.Title(titlePosition, label);

        Rect listPosition = new Rect(position);

        if (!allValid)
        {
            Rect helpBoxPosition = new Rect(position);
            helpBoxPosition.height = EditorGUIUtility.singleLineHeight * 2;
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

        ReorderableListGUI.ListFieldAbsolute(listPosition, property.FindPropertyRelative("strings"));
    }
}
