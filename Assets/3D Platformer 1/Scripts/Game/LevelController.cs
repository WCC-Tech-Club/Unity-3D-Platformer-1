using UnityEngine;

using Codari.CameraControl;

public sealed class LevelController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

    public CameraController CameraController { get { return cameraController; } }
}
