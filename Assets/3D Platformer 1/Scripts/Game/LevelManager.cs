using UnityEngine;

/// <summary>
///		Handles the loading the main menu and all levels of the game.
/// </summary>
public sealed class LevelManager : MonoBehaviour
{
	[SerializeField]
	private string mainMenu;			// Scene name for the main menu.
	[SerializeField]
	private string debugLevel;			// Scene name for the debug level.
	[SerializeField]
	private string[] levels;			// Scene names for the game levels.

    private int? currentLevel;          // Current level in the levels array.

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
    ///     Gets the current level.
    /// </summary>
	/// <remarks>
	///		<para>
	///			This value is <c>null</c> if not loaded in an numeric level already.
	///		</para>
	/// </remarks>
    public int? CurrentLevel { get { return currentLevel; } }

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
	}
}
