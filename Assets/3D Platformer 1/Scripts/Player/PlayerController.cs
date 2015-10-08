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
       
    }

    void FixedUpdate()
    {

    }
}
