using UnityEngine;

using Codari.CameraControl;

public sealed class LevelController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private Transform playerSpawn;

    public CameraController CameraController { get { return cameraController; } }

    public Transform PlayerSpawn { get { return playerSpawn; } }

    void Awake()
    {
        if (cameraController == null)
        {
            bool gameExists = Game.Exists;

            if (Debug.isDebugBuild)
            {
                Debug.LogErrorFormat(this, "<b>Missing CameraController</b>: LevelController for level `{0}` has no `CameraController` reference.",
                    Game.Exists ? Game.LevelManager.CurrentLevelName : "-!-UNKNOWN-!-");
            }
        }

        if (playerSpawn == null)
        {
            bool gameExists = Game.Exists;

            if (Debug.isDebugBuild)
            {
                Debug.LogErrorFormat(this, "<b>Missing PlayerSpawn</b>: LevelController for level `{0}` has no player spawn reference.",
                    Game.Exists ? Game.LevelManager.CurrentLevelName : "-!-UNKNOWN-!-");
            }
        }
    }
}
