using UnityEngine;

// Tutorial step for switching to virtual input mode.
// Requires the user to press the input switch button once,
// then blocks consecutive presses untill the step is completed.
[CreateAssetMenu(menuName = "Tutorial/Virtual Input Switch Step")]
public class VirtualInputSwitchStep : TutorialStepBase
{
    private bool triggered = false;

    public override void OnStepStart(TutorialManager manager)
    {
        triggered = false;
        manager.DisableAllInput();

        requiredAction.action.Enable();
    }

    public override void OnStepUpdate(TutorialManager manager)
    {
        if (triggered)
            return;

 
        if (requiredAction.action.WasPerformedThisFrame())
        {
            triggered = true;

            manager.inputModeSwitcher.switchInput.action.Disable();

            manager.continuePrompt.SetActive(true);
            manager.wasActionPerformed = true;
        }
    }
}
