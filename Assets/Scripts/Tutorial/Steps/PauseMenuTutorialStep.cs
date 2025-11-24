using UnityEngine;

// Tutorial step for opening and closing the pause menu.
// Requires the user to press the pause button twice.
[CreateAssetMenu(menuName = "Tutorial/Pause Menu Step")]
public class PauseMenuTutorialStep : TutorialStepBase
{
    private bool menuOpened = false;

    public override void OnStepStart(TutorialManager manager)
    {
        menuOpened = false;

        manager.DisableAllInput();
        manager.pauseMenuController.pauseInput.action.Enable();
    }

    public override void OnStepUpdate(TutorialManager manager)
    {
        var action = manager.pauseMenuController.pauseInput.action;

        if (action.WasPerformedThisFrame())
        {
            if (!menuOpened)
            {
                menuOpened = true;
            }
            else
            {
                manager.pauseMenuController.pauseInput.action.Disable();
                manager.DisableAllInput();

                manager.continuePrompt.SetActive(true);
                manager.wasActionPerformed = true;
            }
        }
    }

    public override void OnStepComplete(TutorialManager manager)
    {
        manager.pauseMenuController.pauseInput.action.Disable();
    }
}
