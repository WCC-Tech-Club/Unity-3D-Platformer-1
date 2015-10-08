using UnityEngine;

using Codari.CameraControl;

[RequireComponent(typeof(RigidbodyCameraTarget))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private CameraTarget localCameraTarget;
    private PlayerMovement playerMovement;

    private LevelController levelController;

    public CameraTarget CameraTarget { get { return localCameraTarget; } }

    public LevelController LevelController { get { return levelController; } }

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
        if (IsCameraTargeting)
        {
            levelController.CameraController.Pitch += Game.InputManager.CameraPitchAxis;
            levelController.CameraController.Yaw += Game.InputManager.CameraYawAxis;
            levelController.CameraController.Zoom += Game.InputManager.CameraZoomAxis;
        }
    }
}
