using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class VRPauseMenuController : MonoBehaviour
{
    [Header("Menu i interaktory")]
    public GameObject pauseMenuUI;
    public GameObject leftRay;
    public GameObject rightRay;
    public GameObject controller;

    [Header("Wejœcie (Input System)")]
    public InputActionReference pauseInput; // np. przypisz do przycisku Menu / Y / Start

    private bool isPaused = false;

    void Start()
    {
        // Ustaw stan pocz¹tkowy
        pauseMenuUI.SetActive(false);
        SetRayInteractorsActive(false);

        // Subskrybuj event z Input System
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
        isPaused = true;
        Time.timeScale = 0f;

        pauseMenuUI.SetActive(true);
        SetRayInteractorsActive(true);
        DisableController(false);

    }

    private void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        pauseMenuUI.SetActive(false);
        SetRayInteractorsActive(false);
        DisableController(true);
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
}
