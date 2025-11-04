using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class CameraManager : MonoBehaviour
{
    public Camera cameraFPV;
    public Camera camera3rdPerson;
    public GameObject XROrigin;
    public RenderTexture droneViewTexture;
    public GameObject ScreenOn;
    public GameObject ScreenOff;
    public GameObject Vignette;
    public DroneMovement droneMovement;

    public InputActionReference switchViewAction;

    public InputModeSwitcher inputModeSwitcher;

    private bool isFPV = false;
    private bool isVignetteEnabled;
    private bool isSnapRotationEnabled;
    private bool isVirtualInputMode;

    private void Start()
    {
        switchViewAction.action.Enable();
        switchViewAction.action.performed += OnSwitchView;

        isVignetteEnabled = PlayerPrefs.GetInt("Vignette", 0) == 1;
        isSnapRotationEnabled = PlayerPrefs.GetInt("SnapRotation", 0) == 1;

        SetCameraView(isFPV);
    }

    private void OnSwitchView(InputAction.CallbackContext ctx)
    {
        isFPV = !isFPV;
        SetCameraView(isFPV);
    }

    public void SetCameraView(bool useFPV)
    {
        isVignetteEnabled = PlayerPrefs.GetInt("Vignette") == 1;
        isSnapRotationEnabled = PlayerPrefs.GetInt("SnapRotation") == 1;
        isVirtualInputMode = inputModeSwitcher.GetMode();


        if (useFPV)
        {
            ScreenOn.SetActive(false);
            ScreenOff.SetActive(true);
            cameraFPV.enabled = true;
            cameraFPV.targetTexture = null;
            camera3rdPerson.enabled = false;
            XROrigin.SetActive(false);

            if (isVignetteEnabled)
                Vignette.SetActive(true);
            else Vignette.SetActive(false);

            if (isSnapRotationEnabled)
                droneMovement.snapRotation = true;
            else droneMovement.snapRotation = false;

            if (isVirtualInputMode)
            {
                inputModeSwitcher.SetMode(false);
            }

            inputModeSwitcher.switchInput.action.Disable();


        }
        else
        {
            ScreenOn.SetActive(true);
            ScreenOff.SetActive(false);
            XROrigin.SetActive(true);
            camera3rdPerson.enabled = true;
            cameraFPV.enabled = false;

            Vignette.SetActive(false);
            droneMovement.snapRotation = false;


            if (droneViewTexture != null)
            {
                cameraFPV.targetTexture = droneViewTexture;
                cameraFPV.enabled = true;
            }


            if (isVirtualInputMode)
            {
                inputModeSwitcher.SetMode(true);
            }
            
            inputModeSwitcher.switchInput.action.Enable();
        }
    }

    private void OnDestroy()
    {
        switchViewAction.action.performed -= OnSwitchView;

        if (cameraFPV != null)
        {
            cameraFPV.targetTexture = null;
        }
    }

    public bool isFPVEnabled()
    {
        return isFPV;
    }
}