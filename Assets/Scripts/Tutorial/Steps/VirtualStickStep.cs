using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Tutorial step for enabling a single virtual joystick's XR Grab Interactible (left or right).
// Only the specified stick is active during this step.
[CreateAssetMenu(menuName = "Tutorial/Virtual Stick Step")]
public class VirtualStickStep : TutorialStepBase
{
    public enum StickSide { Left, Right }
    public StickSide stick;

    public override void OnStepStart(TutorialManager manager)
    {
        manager.analogLeft.enabled = false;
        manager.analogRight.enabled = false;

        if (stick == StickSide.Left)
            manager.analogLeft.enabled = true;
        else
            manager.analogRight.enabled = true;
    }

    public override void OnStepUpdate(TutorialManager manager)
    {
    }

    public override void OnStepComplete(TutorialManager manager)
    {
        manager.analogLeft.enabled = false;
        manager.analogRight.enabled = false;
    }
}