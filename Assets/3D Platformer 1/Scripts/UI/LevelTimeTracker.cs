using UnityEngine;
using UnityEngine.UI;

public sealed class LevelTimeTracker : MonoBehaviour
{
    [SerializeField]
    private Text currentTimeText;
    [SerializeField]
    private Text bestTimeText;

    void OnEnable()
    {
        // Get the current time.
        float currentTime = Time.timeSinceLevelLoad;

        // Get the current best time.
        float? bestTime = Game.LevelManager.CurrentLevelBestTime;

        // If there is a current best time...
        if (bestTime.HasValue && currentTime < bestTime.Value)
        {
            // ... set the best time to the current time.
            bestTime = currentTime;

            // Set the current time as the new best time
            Game.LevelManager.SetCurrentLevelBestTime(currentTime);
        }

        // Set the current time text to represent the formated current time.
        currentTimeText.text = LevelManager.FormatLevelTime(currentTime);

        // Set the best time text to represent the formated best time.
        bestTimeText.text = LevelManager.FormatLevelTime(bestTime);
    }
}
