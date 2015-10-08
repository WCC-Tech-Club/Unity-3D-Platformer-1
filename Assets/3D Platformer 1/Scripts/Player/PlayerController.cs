using UnityEngine;

using Codari.CameraControl;

[RequireComponent(typeof(RigidbodyCameraTarget))]
public sealed class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;                    // Stores reference to the local Rigidbody.
    private RigidbodyCameraTarget localCameraTarget;    // Stores reference to the local RigidbodyCameraTarget.

    private LevelController levelController;            // Reference to current level controller.

    private bool IsCameraTargeting { get { return levelController.CameraController.Target == localCameraTarget; } }

    void Awake()
    {
        // Reference the local components.
        rigidbody = GetComponent<Rigidbody>();
        localCameraTarget = GetComponent<RigidbodyCameraTarget>();

        // Reference to global components.
        levelController = Game.LevelManager.LevelController;
    }

    void FixedUpdate()
    {
        if (levelController.CameraController.Target == localCameraTarget)
        {

        }
    }
}
