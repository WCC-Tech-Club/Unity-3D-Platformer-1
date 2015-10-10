using UnityEditor;

public static class LevelNameUtility
{
    public static bool IsInBuildSettings(string levelName)
    {
        if (levelName.Length == 0)
        {
            return false;
        }

        EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
        for (int i = 0; i < buildScenes.Length; i++)
        {
            if (buildScenes[i].path.Contains("/" + levelName + ".unity"))
            {
                return true;
            }
        }

        return false;
    }
}
