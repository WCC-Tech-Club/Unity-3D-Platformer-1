using UnityEngine;

using System;

/// <summary>
///		Processes and manages settings for player input.
/// </summary>
public sealed class InputManager : MonoBehaviour
{
    [Serializable]
    private class CameraSettings
    {
        public float pitchAxisSensitivity;
        public float yawAxisSensitivity;
        public float zoomSensitivity;
    }

    [SerializeField]
    private CameraSettings cameraSettings;

    void Update()
    {

    }
}
