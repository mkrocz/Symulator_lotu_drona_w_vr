using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Basic Step")]
public class BasicTutorialStep : TutorialStepBase
{
    public override void OnStepUpdate(TutorialManager manager)
    {
        if (!manager.wasActionPerformed)
        {
            if (actionType == TutorialManager.TutorialActionType.InputAction &&
                requiredAction.action.WasPerformedThisFrame())
            {
                manager.continuePrompt.SetActive(true);
                manager.wasActionPerformed = true;
            }
        }
    }
}
