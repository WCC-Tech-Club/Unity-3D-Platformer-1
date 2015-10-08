using UnityEngine;

using System;

using Codari.CameraControl;

/// <summary>
///		Processes and manages settings for player input.
/// </summary>
public sealed class InputManager : MonoBehaviour
{
    #region Input Settings
    [Serializable]
    private class CameraSettings
    {
        public float pitchAxisSensitivity;
        public float yawAxisSensitivity;
        public float zoomSensitivity;
    }

    [SerializeField]
    private CameraSettings cameraSettings;
    #endregion

    #region Raw Input
    private float horizontal;
    private float vertical;
    private bool jump;

    private float mouseX;
    private float mouseY;
    private float mouseScrollWheel;

    private bool menu;
    private bool restart;

    public float HorizontalAxis { get { return horizontal; } }

    public float VerticalAxis { get { return vertical; } }

    public float MouseX { get { return MouseX; } }

    public float MouseY { get { return MouseY; } }

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
