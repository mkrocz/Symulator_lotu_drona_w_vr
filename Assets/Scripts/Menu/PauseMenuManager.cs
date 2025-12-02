using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


// Handles the pause menu behavior in VR.
// Enables/disables pause UI, ray interactors, drone, screen, and controller.
// Also pauses/resumes time and manages camera view when in FPV mode.
public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject leftRay;
    public GameObject rightRay;
    public GameObject controller;
    public GameObject screen;
    public GameObject drone;

    public InputActionReference pauseInput;

    public CameraManager cameraManager;

    private bool wasFPVActive = false;

    private bool isPaused = false;

    void OnEnable()
    {
        pauseMenuUI.SetActive(false);
        SetRayInteractorsActive(false);

        pauseInput.action.started += OnPausePressed;
        pauseInput.action.Enable();
    }

    void OnDestroy()
    {
        pauseInput.action.started -= OnPausePressed;
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    private void Pause()
    {
        // Remember if FPV was active and switch to 3rd person view
        if (cameraManager != null)
        {
            wasFPVActive = cameraManager.isFPVEnabled();

            if (wasFPVActive)
            {
                cameraManager.SetCameraView(false);
            }
        }


        isPaused = true;
        Time.timeScale = 0f;

        // Show pause menu, enable interaction, disable unnecessary models that could potentially block the view
        pauseMenuUI.SetActive(true);
        SetRayInteractorsActive(true);
        DisableController(false);
        DisableScreen(false);
        DisableDrone(false);


        // Disable camera switch input while in paused state
        cameraManager.switchViewAction.action.Disable();


    }

    private void Resume()
    {
        if (cameraManager != null)
        {
            wasFPVActive = cameraManager.isFPVEnabled();

            if (wasFPVActive)
            {
                cameraManager.SetCameraView(true);
            }
        }


        isPaused = false;
        Time.timeScale = 1f;

        pauseMenuUI.SetActive(false);
        SetRayInteractorsActive(false);
        DisableController(true);
        DisableScreen(true);
        DisableDrone(true);
        cameraManager.switchViewAction.action.Enable();
    }

    private void SetRayInteractorsActive(bool active)
    {
        if (leftRay != null) leftRay.gameObject.SetActive(active);
        if (rightRay != null) rightRay.gameObject.SetActive(active);
    }

    private void DisableController(bool active)
    {
        if (controller != null) controller.gameObject.SetActive(active);
    }

    private void DisableScreen(bool active)
    {
        if (screen != null) screen.gameObject.SetActive(active);
    }

    private void DisableDrone(bool active)
    {
        if (drone != null) drone.gameObject.SetActive(active);
    }

}
