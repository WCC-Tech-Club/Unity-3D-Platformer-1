﻿using UnityEngine;

/// <summary>
///		Core component of the game, primary access to game data.
/// </summary>
[RequireComponent(typeof(InputManager), typeof(LevelManager))]
public sealed class Game : MonoBehaviour
{
	#region Static Access
	private static Game instance;           // Static reference of Game.

	/// <summary>
	///		Checks if an instance of the game component exists.
	/// </summary>
	public static bool Exists { get { return instance != null; } }

	/// <summary>
	///     Gets the existing `Game` instance.
	/// </summary>
	public static Game Instance { get { return instance; } }

	/// <summary>
	///		Gets the <see cref="InputManager"/>.
	/// </summary>
	public static InputManager InputManager { get { return instance.inputManager; } }

	/// <summary>
	///		Gets the <see cref="LevelManager"/>.
	/// </summary>
	public static LevelManager LevelManager { get { return instance.levelManager; } }

	/// <summary>
	///     Checks if the game is currently paused
	/// </summary>
	public static bool Paused
	{
		get { return instance.paused; }
		set { instance.SetPaused(value); }
	}
	#endregion

	private InputManager inputManager;      // Reference to InputManager.
	private LevelManager levelManager;      // Reference if LevelManager.

	private bool paused;                    // Flag that denotes if the game is paused.

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
		levelManager.LoadMainMenu();
	}

	/// <summary>
	///     Sets the paused state of the game.
	/// </summary>
	/// <param name="paused">
	///     The new paused state.
	/// </param>
	public void SetPaused(bool paused)
	{
		// If the new paused state is different than the current paused state...
		if (this.paused != paused)
		{
			// ... toggle the paused state
			TogglePaused();
		}
	}

	/// <summary>
	///     Toggles the paused state of the game.
	/// </summary>
	public void TogglePaused()
	{
		// If currently paused...
		if (paused)
		{
			// ... set paused to false.
			paused = false;
			// Set the time scale to 1.
			Time.timeScale = 1;
		}
		else
		{
			// ... else set paused to true.
			paused = true;
			// Set the time scale to 0.
			Time.timeScale = 0;
		}
	}
}
