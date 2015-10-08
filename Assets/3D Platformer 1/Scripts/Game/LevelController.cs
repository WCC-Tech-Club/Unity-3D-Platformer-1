using UnityEngine;

using Codari.CameraControl;

public sealed class LevelController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

    public CameraController CameraController { get { return cameraController; } }

    void Update()
    {
        // Reload current level if restart button is pressed.
        Game.InputManager.RestartButton(Game.LevelManager.ReloadCurrent);
    }
}
