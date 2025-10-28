using UnityEngine;

public class VirtualInput : MonoBehaviour
{
    public Transform moveLever;
    public Transform controlLever;
    private float maxLeverAngle = 20f;

    public DroneMovement droneMovement;

    void FixedUpdate()
    {
        Vector2 move = GetLeverInput2D(moveLever);
        Vector2 control = GetLeverInput2D(controlLever);

        Vector3 moveVector = new Vector3(move.x, 0, move.y);
        float ascend = control.y;
        float yaw = control.x;

        droneMovement.Move(moveVector, ascend, yaw);
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
