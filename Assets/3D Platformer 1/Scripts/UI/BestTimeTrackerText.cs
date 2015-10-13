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
        // Get the existing best time
        float bestTime = Game.LevelManager.CurrentLevelBestTime;

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
        text.text = bestTime.ToString("0.##") + " Seconds";
    }
}
