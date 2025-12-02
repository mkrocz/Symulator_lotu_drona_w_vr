using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


// Reads controller input actions (move, ascend, descend, rotate)
// and passes the values to the DroneMovement component.
public class ControllerInput : MonoBehaviour
{
    public InputActionReference moveInput;
    public InputActionReference ascendInput;
    public InputActionReference descendInput;
    public InputActionReference rotateInput;
    public DroneMovement droneMovement;

    void OnEnable()
    {
        moveInput.action.Enable();
        ascendInput.action.Enable();
        descendInput.action.Enable();
        rotateInput.action.Enable();


        StartCoroutine(EnableNextFrame());
    }

    void FixedUpdate()
    {
        Vector2 move = moveInput.action.ReadValue<Vector2>();
        float ascend = ascendInput.action.ReadValue<float>();
        float descend = descendInput.action.ReadValue<float>();
        float verticalInput = ascend - descend;

        Vector2 rotate = rotateInput.action.ReadValue<Vector2>();
        float yawInput = rotate.x;

        Vector3 moveVector = new Vector3(move.x, 0, move.y);

        droneMovement.Move(moveVector, verticalInput, yawInput);
    }



    IEnumerator EnableNextFrame()
    {
        yield return null;
        moveInput.action.Enable();
        ascendInput.action.Enable();
        descendInput.action.Enable();
        rotateInput.action.Enable();
    }
}
