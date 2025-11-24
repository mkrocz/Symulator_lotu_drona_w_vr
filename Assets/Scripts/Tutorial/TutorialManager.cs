using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

// Manages the VR tutorial flow using a list of tutorial steps.
// Each step can define its own behavior via TutorialStepBase (ScriptableObject).
// Handles user input, virtual controls, UI prompts, and progression between steps.
public class TutorialManager : MonoBehaviour
{
    public List<TutorialStepBase> steps; // List of steps forming the tutorial
    int currentStep = 10; // Index of the current tutorial step

    public XRGrabInteractable analogLeft;
    public XRGrabInteractable analogRight;
    public GameObject tutorialPanel;
    public TMPro.TextMeshProUGUI instructionText;
    public GameObject continuePrompt;
    public ControllerInput controllerInput;
    public VirtualInputTutorial virtualInput;
    public InputModeSwitcher inputModeSwitcher;
    public PauseMenuController pauseMenuController;
    public CameraManager cameraManager;
    public WindController windController;

    public InputActionReference nextStep;

    [HideInInspector] public bool wasActionPerformed = false; // True if the required action for current step was completed

    public enum TutorialActionType
    {
        InputAction,
        VirtualMove,
        VirtualAscend,
        VirtualYaw
    }

    void Start()
    {
        DisableAllInput();
        nextStep.action.Enable();

        // Subscribe to virtual input callbacks
        virtualInput.OnMove += () => OnVirtualAction(TutorialActionType.VirtualMove);
        virtualInput.OnAscend += () => OnVirtualAction(TutorialActionType.VirtualAscend);
        virtualInput.OnYaw += () => OnVirtualAction(TutorialActionType.VirtualYaw);

        // Disable analog levers initially
        analogLeft.enabled = false;
        analogRight.enabled = false;

        windController.enabled = false;

        StartStep(10);
    }

    // Starts a tutorial step at the given index:
    // - Updates UI text
    // - Hides continue prompt
    // - Disables all unrelated inputs
    // - Enables the required action input for the step
    // - Resets action completion flag
    // - Calls the step's OnStepStart() for custom behavior
    void StartStep(int index)
    {
        currentStep = index;

        var step = steps[currentStep];

        instructionText.text = step.instructionText;
        continuePrompt.SetActive(false);

        DisableAllInput();
        step.requiredAction?.action?.Enable();

        wasActionPerformed = false;

        step.OnStepStart(this);
    }

    // Main tutorial update loop:
    // - Calls the current step's OnStepUpdate() for custom behavior
    // - Checks if the required action was performed and user pressed "next"
    // - Calls OnStepComplete() and advances to next step
    void Update()
    {
        var step = steps[currentStep];

        step.OnStepUpdate(this);

        if (wasActionPerformed && nextStep.action.WasPerformedThisFrame())
        {
            step.OnStepComplete(this);
            NextStep();
        }
    }

    // Advances to the next tutorial step or closes the tutorial panel if finished.
    void NextStep()
    {
        DisableAllInput();

        if (currentStep + 1 < steps.Count)
            StartStep(currentStep + 1);
        else
            tutorialPanel.SetActive(false);
    }

    public void DisableAllInput()
    {
        controllerInput.moveInput.action.Disable();
        controllerInput.ascendInput.action.Disable();
        controllerInput.descendInput.action.Disable();
        controllerInput.rotateInput.action.Disable();
        inputModeSwitcher.switchInput.action.Disable();
        pauseMenuController.pauseInput.action.Disable();
        cameraManager.switchViewAction.action.Disable();
    }

    public void EnableAllInput()
    {
        controllerInput.moveInput.action.Enable();
        controllerInput.ascendInput.action.Enable();
        controllerInput.descendInput.action.Enable();
        controllerInput.rotateInput.action.Enable();
        inputModeSwitcher.switchInput.action.Enable();
        pauseMenuController.pauseInput.action.Enable();
        cameraManager.switchViewAction.action.Enable();
    }

    // Called by virtual input events. Marks the required action as performed
    // if it matches the current step type and shows the continue prompt
    void OnVirtualAction(TutorialActionType type)
    {
        var step = steps[currentStep];

        if (!wasActionPerformed && step.actionType == type)
        {
            continuePrompt.SetActive(true);
            wasActionPerformed = true;
        }
    }
}
