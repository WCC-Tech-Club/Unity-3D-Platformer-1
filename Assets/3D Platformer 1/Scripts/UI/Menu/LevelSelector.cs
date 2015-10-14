using UnityEngine;
using UnityEngine.UI;

using System;

public sealed class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject levelSelectorButtonPrefab;       // Reference to the prefab for the level selection button.

    void Awake()
    {
        // Reference the transform for convienience/slight performance.
        Transform transform = base.transform;

        // For each level in the level manager...
        for (int i = 0; i < Game.LevelManager.LevelCount; i++)
        {
            // ... instantiate (copy and load) a new instance of the level selection button.
            GameObject levelSelectionButton = Instantiate(levelSelectorButtonPrefab);

            // Access the `Text` component within the instantiated object.
            Text text = levelSelectionButton.GetComponentInChildren<Text>();
            // Set the button text to look like "Level <level number>: <level name>".
            text.text =
                "Level " + (i + 1) + ": " + Game.LevelManager.GetLevelName(i) + Environment.NewLine +
                "Best Time: " + Game.LevelManager.GetBestTimeFormated(i);

            // Reference the level index into a value that will never change
            int levelIndex = i;
            // Access the `Button` component within the instantiated object.
            Button button = levelSelectionButton.GetComponentInChildren<Button>();
            // Add a listener for when the button is clicked to load the selected level.
            button.onClick.AddListener(() => { Game.LevelManager.LoadLevel(levelIndex); });

            // Set the parent of the new level selection button to be that of this objects transform.
            levelSelectionButton.transform.SetParent(transform);
        }
    }
}
