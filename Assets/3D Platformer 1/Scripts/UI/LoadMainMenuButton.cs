using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class LoadMainMenuButton : MonoBehaviour
{
    void Awake()
    {
        // Add a listener to the local `Button` component.
        GetComponent<Button>().onClick.AddListener(() =>
        {
            // If game instance exists...
            if (Game.Exists)
            {
                // ... load the main menu.
                Game.LevelManager.LoadMainMenu();
            }
            else
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogErrorFormat(this, "<b>Game Not Found</b>: Level selector could not be obtained as the `Game` instance does not exist.");
                }
            }
        });
    }
}
