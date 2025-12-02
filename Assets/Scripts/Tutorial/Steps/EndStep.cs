using UnityEngine;


[CreateAssetMenu(menuName = "Tutorial/End Step")]
public class EndStep : TutorialStepBase
{

    public override void OnStepStart(TutorialManager manager)
    {
        manager.continuePrompt.SetActive(false);
        manager.wasActionPerformed = true;
        manager.continuePrompt.SetActive(false);
        manager.endPrompt.SetActive(true);
    }

}
