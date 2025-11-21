using UnityEngine;
using UnityEngine.InputSystem;

// Base class for all tutorial steps. Defines the structure and common data for a step.
// Each step can override the following methods to implement custom behavior:
// - OnStepStart: called when the step begins
// - OnStepUpdate: called every frame during the step
// - OnStepComplete: called when the step finishes
public abstract class TutorialStepBase : ScriptableObject
{
    [TextArea]
    public string instructionText;

    public TutorialManager.TutorialActionType actionType;
    public InputActionReference requiredAction;

    public virtual void OnStepStart(TutorialManager manager) { }

    public virtual void OnStepUpdate(TutorialManager manager) { }

    public virtual void OnStepComplete(TutorialManager manager) { }
}
