using UnityEngine;

using Codari.CameraControl;

public sealed class LevelController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private Player player;

    public CameraController CameraController { get { return cameraController; } }

    public Player Player { get { return player; } }

    void Update()
    {
        // Reload current level if restart button is pressed.
        Game.InputManager.RestartButton(Game.LevelManager.ReloadCurrent);
    }
}
