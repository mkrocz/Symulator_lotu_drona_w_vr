using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public Camera cameraFPV;
    public Camera camera3rdPerson;
    public GameObject XROrigin;
    public RenderTexture droneViewTexture;
    public GameObject ScreenOn;
    public GameObject ScreenOff;

    public InputActionReference switchViewAction;

    private bool isFPV = false;

    private void Start()
    {
        switchViewAction.action.Enable();
        switchViewAction.action.performed += OnSwitchView;

        SetCameraView(isFPV);
    }

    private void OnSwitchView(InputAction.CallbackContext ctx)
    {
        isFPV = !isFPV;
        SetCameraView(isFPV);
    }

    private void SetCameraView(bool useFPV)
    {
        if (useFPV)
        {
            ScreenOn.SetActive(false);
            ScreenOff.SetActive(true);
            cameraFPV.enabled = true;
            cameraFPV.targetTexture = null;
            camera3rdPerson.enabled = false;
            XROrigin.SetActive(false);
            
        }
        else
        {
            ScreenOn.SetActive(true);
            ScreenOff.SetActive(false);
            XROrigin.SetActive(true);
            camera3rdPerson.enabled = true;
            cameraFPV.enabled = false;

            if (droneViewTexture != null)
            {
                cameraFPV.targetTexture = droneViewTexture;
                cameraFPV.enabled = true;
            }
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
}