using UnityEngine;

using Codari.CameraControl;

[RequireComponent(typeof(RigidbodyCameraTarget))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private CameraTarget localCameraTarget;         // Reference to local CameraTarget.
    private PlayerMovement playerMovement;          // Reference to local PlayerMovement.

    private LevelController levelController;        // Reference to global LevelController.

    /// <summary>
    ///     Gets the local camera target of the player.
    /// </summary>
    public CameraTarget CameraTarget { get { return localCameraTarget; } }

    /// <summary>
    ///     Gets the level controller of the level the player is in.
    /// </summary>
    public LevelController LevelController { get { return levelController; } }

    /// <summary>
    ///     Checks if the camera controller targeting the player right now.
    /// </summary>
    public bool IsCameraTargeting { get { return levelController.CameraController.Target == localCameraTarget; } }

    void Awake()
    {
        // Reference local components.
        localCameraTarget = GetComponent<RigidbodyCameraTarget>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        // Reference level controller in start as it isnt avaliable in awake.
        levelController = Game.LevelManager.LevelController;
    }

    void FixedUpdate()
    {
        // If camera is targeting the player...
        if (IsCameraTargeting)
        {
            // ... then switch the update mode to fixed in case it wasn't already.
            levelController.CameraController.UpdateMode = UpdateMode.Fixed;

            // Apply camera input values from the input manager to the camera controller.
            levelController.CameraController.Pitch += Game.InputManager.CameraPitchAxis;
            levelController.CameraController.Yaw += Game.InputManager.CameraYawAxis;
            levelController.CameraController.Zoom += Game.InputManager.CameraZoomAxis;

            // Tell the movement component to move.
            playerMovement.Roll(Game.InputManager.HorizontalAxis, Game.InputManager.VerticalAxis, levelController.CameraController.Yaw);
            Game.InputManager.JumpButton(playerMovement.Jump);
        }
    }
}
