using UnityEngine;
using UnityEditor;

using Codari.CameraControl;

public static class MenuItems
{
    private const string GameControllerPath = "Assets/3D Platformer 1/Prefabs/GameController.prefab";   // Path to the game controller prefab.

    private const string StartupScenePath = "Assets/3D Platformer 1/Scenes/_Startup.unity";             // Path to the startup scene.

    private const string RootLevelRequirements = "Assets/3D Platformer 1/Prefabs/LevelRequirements/";   // Root directory of prefabs that are required in a level.

    private static readonly string[] LevelRequirementPrefabs =                                          // Array of prefab files that are required in a level.
    {
        "End Of Level.prefab",
        "LevelController.prefab",
        "Level UI.prefab",
        "Main Camera.prefab",
        "Player.prefab"
    };

    [MenuItem("WCC Tech Club/Game Controller %#g")]
    public static void InspectGameController()
    {
        Object obj = AssetDatabase.LoadAssetAtPath(GameControllerPath, typeof(GameObject));
        if (obj != null)
        {
            Selection.activeObject = obj;
        }
    }

    [MenuItem("WCC Tech Club/Switch To Startup")]
    public static void SwitchToStartupScene()
    {
        if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorApplication.OpenScene(StartupScenePath);
        }
    }

    /// <summary>
    ///     Creates a new scene with all of the required objects added and connected.
    /// </summary>
    [MenuItem("WCC Tech Club/New Game Level")]
    public static void CreateNewGameLevel()
    {
        // Ask the user if they would like to save the current scene if need be, if they do not press cancel...
        if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            // ... then load a new scene.
            EditorApplication.NewScene();

            // We do not want the default `Main Camera` object do destroy that.
            Object.DestroyImmediate(GameObject.Find("Main Camera"));

            // For each required prefab file name...
            foreach (string levelRequirement in LevelRequirementPrefabs)
            {
                // ... load the prefab from the asset database.
                GameObject requiredPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(RootLevelRequirements + levelRequirement);

                // Instantiate it within the new scene connected to the prefab.
                GameObject requiredObject = PrefabUtility.InstantiatePrefab(requiredPrefab) as GameObject;

                // Disconect it from the prefab so changes to the prefab do not effect the newly created object
                PrefabUtility.DisconnectPrefabInstance(requiredObject);
            }

            // Reference the objects that need connecting to each other.
            EndOfLevel endOfLevel = Object.FindObjectOfType<EndOfLevel>();
            LevelController levelController = Object.FindObjectOfType<LevelController>();
            CameraController cameraController = Object.FindObjectOfType<CameraController>();
            Player player = Object.FindObjectOfType<Player>();
            
            // Create a new serialized object of the end of level.
            SerializedObject serializedEndOfLevel = new SerializedObject(endOfLevel);

            // Set the appropriate properties to their correct values.
            serializedEndOfLevel.FindProperty("menuSwitcher").objectReferenceValue = Object.FindObjectOfType<MenuSwitcher>();

            // Apply the changed properties without an undo.
            serializedEndOfLevel.ApplyModifiedPropertiesWithoutUndo();

            // Create a new serialized object of the level controller.
            SerializedObject serializedLevelController = new SerializedObject(levelController);

            // Set the appropriate properties to their correct values.
            serializedLevelController.FindProperty("cameraController").objectReferenceValue = cameraController;
            serializedLevelController.FindProperty("player").objectReferenceValue = player;

            // Apply the changed properties without an undo.
            serializedLevelController.ApplyModifiedPropertiesWithoutUndo();

            // Create a new serialized object of the camera controller.
            SerializedObject serializedCameraController = new SerializedObject(cameraController);

            // Set the appropriate properties to their correct values.
            serializedCameraController.FindProperty("target").objectReferenceValue = player.GetComponent<CameraTarget>();

            // Apply the changed properties without an undo.
            serializedCameraController.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
