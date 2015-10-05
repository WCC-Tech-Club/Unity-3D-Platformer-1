using UnityEngine;
using UnityEngine.UI;

public sealed class LevelSelector : MonoBehaviour
{
	[SerializeField]
	private GameObject levelSelectorButtonPrefab;       // Reference to the prefab for the level selection button.

	void Awake()
	{
        // If the game instance exists...
		if (Game.Exists)
		{
            // ... Reference the level manager for convienience.
			LevelManager levelManager = Game.LevelManager;
            // Reference the transform for convienience/slight performance.
            Transform transform = base.transform;

            // For each level in the level manager...
            for (int i = 0; i < levelManager.LevelCount; i++)
            {
                // ... instantiate (copy and load) a new instance of the level selection button.
                GameObject levelSelectionButton = Instantiate(levelSelectorButtonPrefab);

                // Access the `Text` component within the instantiated object.
                Text text = levelSelectionButton.GetComponentInChildren<Text>();
                // Set the button text to look like "Level <level number>: <level name>".
                text.text = "Level " + (i + 1) + ": " + levelManager.GetLevelName(i);

                // Reference the level index into a value that will never change
                int levelIndex = i;
                // Access the `Button` component within the instantiated object.
                Button button = levelSelectionButton.GetComponentInChildren<Button>();
                // Add a listener for when the button is clicked to load the selected level.
                button.onClick.AddListener(() => { levelManager.LoadLevel(levelIndex); });

                // Set the parent of the new level selection button to be that of this objects transform.
                levelSelectionButton.transform.SetParent(transform);
            }
        }
		else
		{
			if (Debug.isDebugBuild)
			{
				Debug.LogErrorFormat(this, "<b>Game Not Found</b>: Level selector could not be obtained as the `Game` instance does not exist.");
			}
		}
	}
}
