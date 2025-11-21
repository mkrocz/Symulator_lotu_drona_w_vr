using UnityEngine;
using System;

// Reads virtual joystick input from move and control levers for the tutorial.
// Converts lever rotation into normalized 2D input values and triggers events
// when movement exceeds a small threshold.
public class VirtualInputTutorial : MonoBehaviour
{
    public Transform moveLever;
    public Transform controlLever;
    private float maxLeverAngle = 20f;

    public event Action OnMove;
    public event Action OnAscend;
    public event Action OnYaw;

    void FixedUpdate()
    {
        Vector2 move = GetLeverInput2D(moveLever);
        Vector2 control = GetLeverInput2D(controlLever);

        if (move.magnitude > 0.2f)
            OnMove?.Invoke();

        if (Mathf.Abs(control.y) > 0.2f)
            OnAscend?.Invoke();

        if (Mathf.Abs(control.x) > 0.2f)
            OnYaw?.Invoke();
    }

    Vector2 GetLeverInput2D(Transform lever)
    {
        float x = lever.localEulerAngles.z;
        float y = lever.localEulerAngles.x;

        if (x > 180) x -= 360;
        if (y > 180) y -= 360;

        float inputX = Mathf.Clamp(-x / maxLeverAngle, -1f, 1f);
        float inputY = Mathf.Clamp(y / maxLeverAngle, -1f, 1f);

        Vector2 input = new Vector2(inputX, inputY);

        if (input.magnitude > 1f)
            input.Normalize();

        return input;
    }
}
