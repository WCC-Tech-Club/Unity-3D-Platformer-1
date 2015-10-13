using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class LoadNextLevelButton : MonoBehaviour
{
    private int nextLevel = -1;

    void Start()
    {
        // Get current level
        int? currentLevel = Game.LevelManager.CurrentLevel;

        // If we are in a numeric level and there is a next level...
        if (currentLevel.HasValue && (nextLevel = currentLevel.Value + 1) < Game.LevelManager.LevelCount)
        {
            // ... add a listener to the local `Button` component to load next level.
            GetComponent<Button>().onClick.AddListener(LoadNextLevel);
        }
        else
        {
            // Make the button not interactable.
            GetComponent<Button>().interactable = false;
        }
    }

    void LoadNextLevel()
    {
        // Load next level.
        Game.LevelManager.LoadLevel(nextLevel);
    }
}
