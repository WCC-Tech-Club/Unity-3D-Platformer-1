using UnityEngine;
using UnityEngine.UI;

using System;

public sealed class OptionsMenu : MonoBehaviour
{
    [Serializable]
    private class SliderGroup
    {
        public Text label = null;
        public Slider slider = null;

        public bool UnsavedChange { get; private set; }

        public void Init(float min, float max, float value)
        {
            slider.minValue = min;
            slider.maxValue = max;
            slider.value = value;
        }

        public void DefaultValue(float value)
        {
            slider.value = value;
        }

        public void RevertValue(float value)
        {
            slider.value = value;
            label.fontStyle = FontStyle.Normal;
        }

        public void SaveValue(Action<float> saveAction)
        {
            saveAction(slider.value);
            label.fontStyle = FontStyle.Normal;
        }

        public void CheckForChange(float original)
        {
            if (slider.value == original)
            {
                label.fontStyle = FontStyle.Normal;
                UnsavedChange = false;
            }
            else
            {
                label.fontStyle = FontStyle.BoldAndItalic;
                UnsavedChange = true;
            }
        }
    }

    [Serializable]
    private class ToggleGroup
    {
        public Toggle toggle = null;
        public Text label = null;

        public bool UnsavedChange { get; private set; }

        public void Init(bool value)
        {
            toggle.isOn = value;
        }

        public void DefaultValue(bool value)
        {
            toggle.isOn = value;
        }

        public void RevertValue(bool value)
        {
            toggle.isOn = value;
            label.fontStyle = FontStyle.Normal;
        }

        public void SaveValue(Action<bool> saveAction)
        {
            saveAction(toggle.isOn);
            label.fontStyle = FontStyle.Normal;
        }

        public void CheckForChange(bool original)
        {
            if (toggle.isOn == original)
            {
                label.fontStyle = FontStyle.Normal;
                UnsavedChange = false;
            }
            else
            {
                label.fontStyle = FontStyle.BoldAndItalic;
                UnsavedChange = true;
            }
        }
    }

    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button revertButton;
    [SerializeField]
    private Button defaultsButton;

    [SerializeField]
    private SliderGroup cameraPitchAxisSensitivityOption;
    [SerializeField]
    private SliderGroup cameraYawAxisSensitivityOption;
    [SerializeField]
    private SliderGroup cameraZoomAxisSensitivityOption;
    [SerializeField]
    private ToggleGroup cameraInvertZoomOption;


    void Awake()
    {
        saveButton.onClick.AddListener(Save);
        revertButton.onClick.AddListener(Revert);
        defaultsButton.onClick.AddListener(Defaults);

        cameraPitchAxisSensitivityOption.Init(InputManager.MinCameraSensitivity, InputManager.MaxCameraSensitivity, Game.InputManager.CameraSettings.PitchAxisSensitivity);
        cameraYawAxisSensitivityOption.Init(InputManager.MinCameraSensitivity, InputManager.MaxCameraSensitivity, Game.InputManager.CameraSettings.YawAxisSensitivity);
        cameraZoomAxisSensitivityOption.Init(InputManager.MinCameraSensitivity, InputManager.MaxCameraSensitivity, Game.InputManager.CameraSettings.ZoomAxisSensitivity);
        cameraInvertZoomOption.Init(Game.InputManager.CameraSettings.InvertZoom);
    }

    void Update()
    {
        cameraPitchAxisSensitivityOption.CheckForChange(Game.InputManager.CameraSettings.PitchAxisSensitivity);
        cameraYawAxisSensitivityOption.CheckForChange(Game.InputManager.CameraSettings.YawAxisSensitivity);
        cameraZoomAxisSensitivityOption.CheckForChange(Game.InputManager.CameraSettings.ZoomAxisSensitivity);
        cameraInvertZoomOption.CheckForChange(Game.InputManager.CameraSettings.InvertZoom);

        if (cameraPitchAxisSensitivityOption.UnsavedChange ||
            cameraYawAxisSensitivityOption.UnsavedChange ||
            cameraZoomAxisSensitivityOption.UnsavedChange ||
            cameraInvertZoomOption.UnsavedChange)
        {
            saveButton.interactable = true;
            revertButton.interactable = true;
        }
        else
        {
            saveButton.interactable = false;
            revertButton.interactable = false;
        }
    }

    private void Revert()
    {
        cameraPitchAxisSensitivityOption.RevertValue(Game.InputManager.CameraSettings.PitchAxisSensitivity);
        cameraYawAxisSensitivityOption.RevertValue(Game.InputManager.CameraSettings.YawAxisSensitivity);
        cameraZoomAxisSensitivityOption.RevertValue(Game.InputManager.CameraSettings.ZoomAxisSensitivity);
        cameraInvertZoomOption.RevertValue(Game.InputManager.CameraSettings.InvertZoom);
    }

    private void Save()
    {
        cameraPitchAxisSensitivityOption.SaveValue((value) => { Game.InputManager.CameraSettings.PitchAxisSensitivity = value; });
        cameraYawAxisSensitivityOption.SaveValue((value) => { Game.InputManager.CameraSettings.YawAxisSensitivity = value; });
        cameraZoomAxisSensitivityOption.SaveValue((value) => { Game.InputManager.CameraSettings.ZoomAxisSensitivity = value; });
        cameraInvertZoomOption.SaveValue((value) => { Game.InputManager.CameraSettings.InvertZoom = value; });
    }

    private void Defaults()
    {
        cameraPitchAxisSensitivityOption.DefaultValue(InputManager.DefaultCameraPitchAxisSensitivity);
        cameraYawAxisSensitivityOption.DefaultValue(InputManager.DefaultCameraYawAxisSensitivity);
        cameraZoomAxisSensitivityOption.DefaultValue(InputManager.DefaultCameraZoomAxisSensitivity);
        cameraInvertZoomOption.DefaultValue(InputManager.DefaultCameraInvertZoom == 1);
    }
}
