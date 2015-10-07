using UnityEngine;

using Codari.CameraControl;

[RequireComponent(typeof(RigidbodyCameraTarget))]
public sealed class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;                    // Stores reference to the local Rigidbody.
    private RigidbodyCameraTarget localCameraTarget;    // Stores reference to the local RigidbodyCameraTarget.

    private InputManager inputManager;                  // Stores reference to the global InputManager.
    private CameraController cameraController;          // Stores reference to the global CameraController.

    void Awake()
    {
        // Reference the local components.
        rigidbody = GetComponent<Rigidbody>();
        localCameraTarget = GetComponent<RigidbodyCameraTarget>();
    }

    void OnEnable()
    {
        // If game instance exists...
        if (Game.Exists)
        {
            // ... reference the game's input manager.
            inputManager = Game.InputManager;
        }
        else
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogErrorFormat(this, "<b>Game Not Found</b>: Input manager could not be obtained as the `Game` instance does not exist.");
            }

            // ... else disable the `PlayerController`.
            enabled = false;
            return;
        }

        // If level manager contains a level controller for the current level...
        if (Game.LevelManager.LevelControllerExists)
        {
            cameraController = Game.LevelManager.LevelController.CameraController;
        }
        else
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogErrorFormat(this, "<b>LevelController Not Found</b>: Current level `{0}` does not contain a `LevelController`.",
                    Game.LevelManager.CurrentLevelName);
            }

            // ... else disable the `PlayerController`.
            enabled = false;
            return;
        }
    }

    void FixedUpdate()
    {

    }
}
