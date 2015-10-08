using UnityEngine;

using Codari.CameraControl;

public sealed class LevelController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private Player player;

    /// <summary>
    ///     Gets the camera controller of the current level.
    /// </summary>
    public CameraController CameraController { get { return cameraController; } }

    /// <summary>
    ///     Gets the player in the current level.
    /// </summary>
    public Player Player { get { return player; } }

    void Awake()
    {
        if (cameraController == null)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogErrorFormat(this, "<b>Missing CameraController</b>: Level `{0}` is missing a camera controller, returning to {1}.",
                    (Game.LevelManager.CurrentLevel.HasValue ? "[" + Game.LevelManager.CurrentLevel.Value + "]" : string.Empty) + Game.LevelManager.CurrentLevelName,
                    Game.LevelManager.MainMenuName);
            }

            Game.LevelManager.LoadMainMenu();
            return;
        }

        if (cameraController == null)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogErrorFormat(this, "<b>Missing Player</b>: Level `{0}` is missing a player, returning to {1}.",
                    (Game.LevelManager.CurrentLevel.HasValue ? "[" + Game.LevelManager.CurrentLevel.Value + "]" : string.Empty) + Game.LevelManager.CurrentLevelName,
                    Game.LevelManager.MainMenuName);
            }

            Game.LevelManager.LoadMainMenu();
            return;
        }
    }

    void Update()
    {
        // Reload current level if restart button is pressed.
        Game.InputManager.RestartButton(Restart);
    }

    /// <summary>
    ///     Reloads the level to a fresh start.
    /// </summary>
    public void Restart()
    {
        Game.LevelManager.ReloadCurrent();
    }
}
