using UnityEngine;

using Codari.CameraControl;

[RequireComponent(typeof(RigidbodyCameraTarget))]
public sealed class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;                // Reference to `Rigidbody`.
    private InputManager inputManager;              // Reference to `InputManager`.
    private CameraController cameraController;      // Reference to `CameraController`.

    void Awake()
    {
        // Reference the local `Rigidbody` component.
        rigidbody = GetComponent<Rigidbody>();
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

        // If the there is a main camera...
        if (Camera.main != null)
        {
            // ... then obtain its `CameraController`.
            cameraController = Camera.main.GetComponent<CameraController>();

            // If the camera controller was not obtained...
            if (cameraController != null)
            {
                // ... then set this object as its target.
                cameraController.Target = GetComponent<RigidbodyCameraTarget>();

                // Set it to manual update mode if not already.
                cameraController.UpdateMode = UpdateMode.Manual;
            }
            else
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogErrorFormat(this, "<b>CameraController Not Found</b>: No `CameraController` was found attached to the `Main Camera`.");
                }

                // ... else disable the `PlayerController`.
                enabled = false;
                return;
            }
        }
        else
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogErrorFormat(this, "<b>Main Camera Not Found</b>: No `Main Camera` was found in the current scene.");
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
