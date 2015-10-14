using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public sealed class BestTimeTrackerText : MonoBehaviour
{
    private Text text;          // Reference to local Text component.

    void Awake()
    {
        // Reference local Text component.
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        // If we are in a numeric level...
        if (Game.LevelManager.CurrentLevel.HasValue)
        {
            // ... get the current best time.
            float bestTime = Game.LevelManager.GetBestTime(Game.LevelManager.CurrentLevel.Value);

            // Get the current time.
            float currentTime = Time.timeSinceLevelLoad;

            // If the current time is better...
            if (currentTime < bestTime)
            {
                // ... set the best time to the current time.
                bestTime = currentTime;

                // Store the new best time in the player prefs for the level.
                PlayerPrefs.SetFloat(Game.LevelManager.CurrentLevelName, bestTime);
            }

            // Set the text to represent the formated best time.
            text.text = LevelManager.FormatTime(bestTime);
        }
        else
        {
            // ... else set text object active to false.
            text.text = "N/A";
        }
    }
}
