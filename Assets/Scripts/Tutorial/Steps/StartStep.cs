using UnityEngine;

// Starting tutorial step.
// Requires the user to press the "next" button twice, separating two presses.
[CreateAssetMenu(menuName = "Tutorial/Start Step")]
public class StartStep : TutorialStepBase
{
    private bool firstPressDone = false;

    public override void OnStepStart(TutorialManager manager)
    {
        firstPressDone = false;

        manager.continuePrompt.SetActive(false);

        if (actionType == TutorialManager.TutorialActionType.InputAction)
            requiredAction.action.Enable();
    }

    // Called every frame. Tracks the two presses of the required action:
    // - First press shows the continue prompt
    // - Second press sets wasActionPerformed = true, allowing the tutorial to advance
    public override void OnStepUpdate(TutorialManager manager)
    {
        if (!firstPressDone && requiredAction.action.WasPerformedThisFrame())
        {
            manager.continuePrompt.SetActive(true);
            firstPressDone = true;
        }
        else if (firstPressDone && requiredAction.action.WasPerformedThisFrame())
        {
            manager.wasActionPerformed = true;
        }
    }

    public override void OnStepComplete(TutorialManager manager)
    {
        manager.continuePrompt.SetActive(false);
    }
}
