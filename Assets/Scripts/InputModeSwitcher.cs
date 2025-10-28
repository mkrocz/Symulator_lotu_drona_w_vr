using UnityEngine;
using UnityEngine.InputSystem;

public class InputModeSwitcher : MonoBehaviour
{
    public ControllerInput controllerInput;
    public VirtualInput virtualInput;
    public InputActionReference switchInput;

    private bool isVR = false;

    void Start()
    {
        SetMode(isVR);
        switchInput.action.started += OnSwitchInputPressed;
        switchInput.action.Enable();
    }

    void OnDestroy()
    {
        switchInput.action.started -= OnSwitchInputPressed;
    }

    void OnSwitchInputPressed(InputAction.CallbackContext context)
    {
        isVR = !isVR;
        SetMode(isVR);
    }

    void SetMode(bool vrMode)
    {
        virtualInput.enabled = vrMode;
        controllerInput.enabled = !vrMode;

        Debug.Log("Tryb sterowania: " + (vrMode ? "VR Levers" : "Gamepad"));
    }
}
