using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public sealed class CurrentLevelText : MonoBehaviour
{
    private Text text;          // Reference to local Text component.

    void Awake()
    {
        // Reference local Text component.
        text = GetComponent<Text>();
    }

    void Start()
    {
        // Get the current level nullable int value.
        int? currentLevel = Game.LevelManager.CurrentLevel;

        // Set the text UI element to the name of the current level, including the numeric level number if possible.
        text.text = "Level " + (currentLevel.HasValue ? currentLevel.Value + ": " : string.Empty) + Game.LevelManager.CurrentLevelName;
    }
}
