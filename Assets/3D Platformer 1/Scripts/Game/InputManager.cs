using UnityEngine;

using System;

/// <summary>
///		Processes and manages settings for player input.
/// </summary>
public sealed class InputManager : MonoBehaviour
{
    #region Input Settings
    public const float MinCameraSensitivity = 1;        // Minimum camera sensitivity value.
    public const float MaxCameraSensitivity = 50;       // Maximum camera sensitivity value.

    [Serializable]
    private class CameraInputSettings : ICameraSettings
    {
        [Range(MinCameraSensitivity, MaxCameraSensitivity)]
        public float pitchAxisSensitivity = 20;                 // Pitch axis sensitivity.

        [Range(MinCameraSensitivity, MaxCameraSensitivity)]
        public float yawAxisSensitivity = 20;                   // Yaw axis sensitivity.

        [Range(MinCameraSensitivity, MaxCameraSensitivity)]
        public float zoomAxisSensitivity = 10;                  // Zoom axis sensitivity.

        public float PitchAxisSensitivity
        {
            get { return pitchAxisSensitivity; }
            set { pitchAxisSensitivity = Mathf.Clamp(value, MinCameraSensitivity, MaxCameraSensitivity); }
        }

        public float YawAxisSensitivity
        {
            get { return yawAxisSensitivity; }
            set { yawAxisSensitivity = Mathf.Clamp(value, MinCameraSensitivity, MaxCameraSensitivity); }
        }

        public float ZoomAxisSensitivity
        {
            get { return zoomAxisSensitivity; }
            set { zoomAxisSensitivity = Mathf.Clamp(value, MinCameraSensitivity, MaxCameraSensitivity); }
        }

        public void Validate()
        {
            PitchAxisSensitivity = pitchAxisSensitivity;
            YawAxisSensitivity = yawAxisSensitivity;
            ZoomAxisSensitivity = zoomAxisSensitivity;
        }
    }

    [SerializeField]
    private CameraInputSettings cameraSettings;

    void OnValidate()
    {
        cameraSettings.Validate();
    }
    #endregion

    #region Raw Input
    private float horizontal;           // Horizontal axis input value.
    private float vertical;             // Vertical axis input value.
    private bool jump;                  // jump button input value.

    private float mouseX;               // Mouse X axis input value.
    private float mouseY;               // Mouse Y axis input value.
    private float mouseScrollWheel;     // Mouse Scroll wheel input value.

    private bool menu;                  // Menu button input value.
    private bool restart;               // Restart button imput value.

    public float HorizontalAxis { get { return horizontal; } }

    public float VerticalAxis { get { return vertical; } }

    public float MouseX { get { return mouseX; } }

    public float MouseY { get { return mouseY; } }

    public float MouseScrollWheel { get { return mouseScrollWheel; } }

    public void JumpButton(Action jumpAction)
    {
        ButtonAction(jumpAction, ref jump);
    }

    public void MenuButton(Action menuAction)
    {
        ButtonAction(menuAction, ref menu);
    }

    public void RestartButton(Action restartAction)
    {
        ButtonAction(restartAction, ref restart);
    }
    #endregion

    #region CameraInput
    public ICameraSettings CameraSettings { get { return cameraSettings; } }

    public float CameraPitchAxis { get { return MouseY * cameraSettings.pitchAxisSensitivity; } }

    public float CameraYawAxis { get { return MouseX * cameraSettings.yawAxisSensitivity; } }

    public float CameraZoomAxis { get { return MouseScrollWheel * cameraSettings.zoomAxisSensitivity; } }
    #endregion

    #region Input Processing
    void Update()
    {
        InputRawAxis("Horizontal", ref horizontal);
        InputRawAxis("Vertical", ref vertical);
        InputButtonDown("Jump", ref jump);

        InputRawAxis("Mouse X", ref mouseX);
        InputRawAxis("Mouse Y", ref mouseY);
        InputRawAxis("Mouse ScrollWheel", ref mouseScrollWheel);

        InputButtonDown("Menu", ref menu);
        InputButtonDown("Restart", ref restart);
    }

    private void InputRawAxis(string name, ref float value)
    {
        value = Input.GetAxisRaw(name);
    }

    private void InputButtonDown(string name, ref bool value)
    {
        value = value || Input.GetButtonDown(name);
    }

    private void ButtonAction(Action action, ref bool value)
    {
        if (value)
        {
            action();
            value = false;
        }
    }
    #endregion
}

public interface ICameraSettings
{
    float PitchAxisSensitivity { get; set; }
    
    float YawAxisSensitivity { get; set; }

    float ZoomAxisSensitivity { get; set; }
}
