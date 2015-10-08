using UnityEngine;

using Codari.CameraControl;

[RequireComponent(typeof(RigidbodyCameraTarget))]
public class Player : MonoBehaviour
{
    private CameraTarget localCameraTarget;
    private LevelController levelController;

    public CameraTarget CameraTarget { get { return localCameraTarget; } }

    public LevelController LevelController { get { return levelController; } }

    public bool IsCameraTargeting { get { return levelController.CameraController.Target == localCameraTarget; } }

    void Awake()
    {
        localCameraTarget = GetComponent<RigidbodyCameraTarget>();
        levelController = Game.LevelManager.LevelController;
    }

    void FixedUpdate()
    {
        if (IsCameraTargeting)
        {

        }
    }
}
