using UnityEngine;
using UnityEditor;

using Codari.CameraControl;

public static class LevelEditorUtility
{
    private const string RootLevelRequirements = "Assets/3D Platformer 1/Prefabs/LevelRequirements/";
    private static readonly string[] LevelRequirementPrefabs = { "LevelController.prefab", "Level UI.prefab", "Main Camera.prefab", "Player.prefab"};

    [MenuItem("WCC Tech Club/New Game Level")]
    public static void CreateNewGameLevel()
    {
        if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorApplication.NewScene();

            Object.DestroyImmediate(GameObject.Find("Main Camera"));

            Transform requiredRoot = new GameObject("Level Requirements").transform;
            foreach (string levelRequirement in LevelRequirementPrefabs)
            {
                GameObject requiredPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(RootLevelRequirements + levelRequirement);
                GameObject requiredObject = PrefabUtility.InstantiatePrefab(requiredPrefab) as GameObject;
                PrefabUtility.DisconnectPrefabInstance(requiredObject);
                requiredObject.transform.SetParent(requiredRoot);
            }

            LevelController levelController = Object.FindObjectOfType<LevelController>();
            CameraController cameraController = Object.FindObjectOfType<CameraController>();
            Player player = Object.FindObjectOfType<Player>();

            SerializedObject serializedLevelController = new SerializedObject(levelController);
            serializedLevelController.FindProperty("cameraController").objectReferenceValue = cameraController;
            serializedLevelController.FindProperty("player").objectReferenceValue = player;
            serializedLevelController.ApplyModifiedPropertiesWithoutUndo();

            SerializedObject serializedCameraController = new SerializedObject(cameraController);
            serializedCameraController.FindProperty("target").objectReferenceValue = player.GetComponent<CameraTarget>();
            serializedCameraController.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
