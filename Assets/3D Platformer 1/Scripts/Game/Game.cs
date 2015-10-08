using UnityEngine;

/// <summary>
///		Core component of the game, primary access to game data.
/// </summary>
[RequireComponent(typeof(InputManager), typeof(LevelManager))]
public sealed class Game : MonoBehaviour
{
    private static Game game;               // Static reference of Game.

    /*
     This method validates the existence of the `Game` instance on startup, if not found then will quit the application or exit play mode.
     */
    [RuntimeInitializeOnLoadMethod]
    static void ValidateGameExists()
    {
        // If instance does not exist...
        if (game == null)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogError("<b>Game Not Found</b>: No instance of `Game` found on launch so application will now quit.");
            }

#if UNITY_EDITOR
            // ... quit game in a delayed call.
            UnityEditor.EditorApplication.delayCall += Quit;
#else
            // ... simply quit the game.
            Quit();
#endif
        }
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        // Exit play mode if in the unity editor.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application if outside the unity editor.
		Application.Quit();
#endif
    }

    /// <summary>
    ///		Gets the <see cref="InputManager"/>.
    /// </summary>
    public static InputManager InputManager { get { return game.inputManager; } }

    /// <summary>
    ///		Gets the <see cref="LevelManager"/>.
    /// </summary>
    public static LevelManager LevelManager { get { return game.levelManager; } }

    /// <summary>
    ///     Checks if the game is currently paused
    /// </summary>
    public static bool Paused
    {
        get { return game.paused; }
        set
        {
            // If the new paused state is different than the current paused state...
            if (game.paused != value)
            {
                // ... toggle the paused state
                TogglePaused();
            }
        }
    }

    /// <summary>
    ///     Toggles the paused state of the game.
    /// </summary>
    public static void TogglePaused()
    {
        // If currently paused...
        if (game.paused)
        {
            // ... set paused to false.
            game.paused = false;
            // Set the time scale to 1.
            Time.timeScale = 1;

            // Hide the cursor while unpaused.
            Cursor.visible = false;
            // Lock the cursor while playing.
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // ... else set paused to true.
            game.paused = true;
            // Set the time scale to 0.
            Time.timeScale = 0;

            // Make the cursor visible while paused.
            Cursor.visible = true;
            // Release the cursor while paused.
            Cursor.lockState = CursorLockMode.None;
        }
    }

#region Game Internal
    private InputManager inputManager;      // Reference to InputManager.
    private LevelManager levelManager;      // Reference to LevelManager.

    private bool paused;                    // Flag that denotes if the game is paused.

    void Awake()
    {
        // Declare static reference.
        game = this;

        // Obtain and reference other game managers.
        inputManager = GetComponent<InputManager>();
        levelManager = GetComponent<LevelManager>();

        // Set to persist when changing scenes.
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Load the main menu.
        levelManager.LoadMainMenu();
    }

    void OnDestroy()
    {
        // If this is the global instance...
        if (this == game)
        {
            // ... destory the entire object.
            Destroy(gameObject);

            // Set instance to null;
            game = null;
        }
    }
#endregion
}
