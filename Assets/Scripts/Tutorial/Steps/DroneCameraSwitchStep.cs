using UnityEngine;

// Tutorial step for switching to drone camera view and back.
// Requires the user to press the camera switch button twice.
[CreateAssetMenu(menuName = "Tutorial/Drone Camera Switch Step")]
public class DroneCameraSwitchStep : TutorialStepBase
{
    private int pressCount = 0;

    public override void OnStepStart(TutorialManager manager)
    {
        pressCount = 0;

        manager.DisableAllInput();

        manager.cameraManager.switchViewAction.action.Enable();
    }

    public override void OnStepUpdate(TutorialManager manager)
    {
        var switchAction = manager.cameraManager.switchViewAction.action;

        if (!switchAction.WasPerformedThisFrame())
            return;

        pressCount++;

        if (pressCount == 1)
        {
            manager.inputModeSwitcher.switchInput.action.Disable();
            manager.pauseMenuController.pauseInput.action.Disable();

            return;
        }

        if (pressCount == 2)
        {

            manager.cameraManager.switchViewAction.action.Disable();
            manager.continuePrompt.SetActive(true);
            manager.wasActionPerformed = true;

            return;
        }
    }

    public override void OnStepComplete(TutorialManager manager)
    {
        manager.cameraManager.switchViewAction.action.Disable();
    }
}
