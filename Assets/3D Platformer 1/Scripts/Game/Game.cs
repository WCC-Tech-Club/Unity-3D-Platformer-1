using UnityEngine;

/// <summary>
///		Core component of the game, primary access to game data.
/// </summary>
[RequireComponent(typeof(InputManager), typeof(LevelManager))]
public sealed class Game : MonoBehaviour
{
	#region Static Access
	private static Game instance;			// Static reference of Game.

	/// <summary>
	///		Gets the <see cref="InputManager"/>.
	/// </summary>
	public static InputManager InputManager { get { return instance.inputManager; } }

	/// <summary>
	///		Gets the <see cref="LevelManager"/>.
	/// </summary>
	public static LevelManager LevelManager { get { return instance.levelManager; } }
	#endregion

	private InputManager inputManager;		// Reference to InputManager.
	private LevelManager levelManager;      // Reference if LevelManager.

	void Awake()
	{
		// Declare static reference.
		instance = this;

		// Obtain and reference other game managers.
		inputManager = GetComponent<InputManager>();
		levelManager = GetComponent<LevelManager>();

		// Set to persist when changing scenes.
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		// Load the main menu.
		// TODO Eventually
		//levelManager.LoadMainMenu();

		// For now load debug.
		levelManager.LoadDebugLevel();
	}
}
