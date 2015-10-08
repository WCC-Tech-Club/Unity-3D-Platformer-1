using UnityEngine;

/// <summary>
///		Handles the loading the main menu and all levels of the game.
/// </summary>
public sealed class LevelManager : MonoBehaviour
{
    /// <summary>
    ///		Enum to indicate the type of level.
    /// </summary>
    public enum LevelType : byte
    {
        /// <summary>
        ///		This type of level is the main menu.
        /// </summary>
        MainMenu,
        /// <summary>
        ///		This type of level is the debug level.
        /// </summary>
        DebugLevel,
        /// <summary>
        ///		This type of level is a numeric level within the indexed levels.
        /// </summary>
        NumericLevel
    }

    [SerializeField]
    private string mainMenu;                    // Scene name for the main menu.
    [SerializeField]
    private string debugLevel;                  // Scene name for the debug level.
    [SerializeField]
    private string[] levels;                    // Scene names for the game levels.

    private LevelType currentLevelType;         // Current level type to identify from.
    private int? currentLevel;                  // Current level in the levels array.

    private LevelController levelController;    // Reference to current LevelController.

    /// <summary>
    ///		Gets the name of the main menu.
    /// </summary>
    /// <remarks>
    ///		<para>
    ///			Although the main menu is not a level to the player, internally it is just
    ///			a "level" with nothing more then a UI.
    ///		</para>
    /// </remarks>
    public string MainMenuName { get { return mainMenu; } }

    /// <summary>
    ///		Gets the name of the debug level.
    /// </summary>
    public string DebugLevelName { get { return debugLevel; } }

    /// <summary>
    ///		Gets the number of levels managed by the level manager.
    /// </summary>
    public int LevelCount { get { return levels.Length; } }

    /// <summary>
    ///		Gets the level type currently loaded.
    /// </summary>
    public LevelType CurrentLevelType { get { return currentLevelType; } }

    /// <summary>
    ///     Gets the current level.
    /// </summary>
    /// <remarks>
    ///		<para>
    ///			This value is <c>null</c> if not loaded in an numeric level already.
    ///		</para>
    /// </remarks>
    public int? CurrentLevel { get { return currentLevel; } }

    /// <summary>
    ///     Checks if there is a <see cref="LevelController"/>.
    /// </summary>
    public bool LevelControllerExists { get { return levelController != null; } }

    /// <summary>
    ///     Gets the current level's <see cref="LevelController"/>.
    /// </summary>
    public LevelController LevelController { get { return levelController; } }

    /// <summary>
    ///		Gets the current loaded levels name.
    /// </summary>
    public string CurrentLevelName
    {
        get
        {
            switch (currentLevelType)
            {
            case LevelType.MainMenu:
                return mainMenu;
            case LevelType.DebugLevel:
                return debugLevel;
            case LevelType.NumericLevel:
                return levels[currentLevel.Value];
            default:
                if (Debug.isDebugBuild)
                {
                    Debug.LogErrorFormat(this, "<b>THIS SHOULDNT HAPPEN</b>: A default case in a enum switch statement that shouldn't happen has happened. " +
                        "For debug purposes the enum value is `{0}` and for some reason if it has a name that is `{1}`",
                        (byte) CurrentLevelType, System.Enum.GetName(typeof(LevelType), currentLevelType));
                }
                return null;
            }
        }
    }

    /// <summary>
    ///		Loads the main menu.
    /// </summary>
    /// <remarks>
    ///		<para>
    ///			If the name returned by <see cref="MainMenuName"/> does not match the name of
    ///			a level built by unity then the level will not load an an error will be logged.
    ///		</para>
    /// </remarks>
    public void LoadMainMenu()
    {
        Application.LoadLevel(mainMenu);
        currentLevel = null;
        currentLevelType = LevelType.MainMenu;
    }

    /// <summary>
    ///		Loads the debug level.
    /// </summary>
    /// <remarks>
    ///		<para>
    ///			If the name returned by <see cref="DebugLevelName"/> does not match the name of
    ///			a level built by unity then the level will not load an an error will be logged.
    ///		</para>
    /// </remarks>
    public void LoadDebugLevel()
    {
        Application.LoadLevel(debugLevel);
        currentLevel = null;
        currentLevelType = LevelType.DebugLevel;
    }

    /// <summary>
    ///		Gets the name of the given level.
    /// </summary>
    /// <remarks>
    ///		<para>
    ///			Throws an exception if <paramref name="level"/> is less than 0 or
    ///			greater than or equal to <see cref="LevelCount"/>.
    ///		</para>
    /// </remarks>
    /// <param name="level">
    ///		Level to get the name for.
    /// </param>
    /// <returns>
    ///		Returns the name of the given level.
    /// </returns>
    public string GetLevelName(int level)
    {
        return levels[level];
    }

    /// <summary>
    ///		Loads the given level.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Levels are loaded starting from 0 onward.
    ///     </para>
    ///		<para>
    ///			If the name returned by <see cref="GetLevelName(int)"/> does not match the name of
    ///			a level built by unity then the level will not load an an error will be logged.
    ///		</para>
    /// </remarks>
    /// <param name="level">
    ///		Level to load.
    /// </param>
    public void LoadLevel(int level)
    {
        Application.LoadLevel(levels[level]);
        currentLevel = level;
        currentLevelType = LevelType.NumericLevel;
    }

    void OnLevelWasLoaded(int level)
    {
        // Find the object taged "LevelController".
        GameObject levelControllerObject = GameObject.FindGameObjectWithTag("LevelController");
        
        // If it exists...
        if (levelControllerObject != null)
        {
            // ... then get the LevelController component from it, will return null if not attached.
            levelController = levelControllerObject.GetComponent<LevelController>();
        }
        else
        {
            // ... else set level controller to null.
            levelController = null;
        }

        // If current level requires a level controller but one was not found...
        if (levelController == null && currentLevelType.RequiresLevelController())
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogErrorFormat(this, "<b>LevelController Not Found</b>: Loaded level `{0}` requires a `LevelController` but does not have one, returning to {1}.",
                    (currentLevel.HasValue ? "[" + currentLevel.Value + "]" : string.Empty) + CurrentLevelName, mainMenu);
            }

            // ... return to the main menu.
            LoadMainMenu();
        }
    }
}

public static class LevelTypeExtensions
{
    public static bool RequiresLevelController(this LevelManager.LevelType levelType)
    {
        switch (levelType)
        {
        case LevelManager.LevelType.NumericLevel:
        case LevelManager.LevelType.DebugLevel:
            return true;
        case LevelManager.LevelType.MainMenu:
        default:
            return false;
        }
    }
}
