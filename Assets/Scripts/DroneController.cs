using UnityEngine;
using UnityEngine.InputSystem;

public class DroneController : MonoBehaviour
{
    public float ascendSpeed = 1.5f;
    public float moveSpeed = 2f;
    public float rotateSpeed = 90f;
    public float snapAngle = 45f;
    public bool smoothRotation = true;
    public float snapCooldown = 0.3f;
    private float nextSnapTime = 0f;

    private Rigidbody rb;

    public Animator[] rotors;

    public InputActionReference moveInput;
    public InputActionReference ascendInput;
    public InputActionReference descendInput;
    public InputActionReference rotateInput;


    void UpdateRotors(bool running)
    {
        foreach (var rotor in rotors)
        {
            rotor.enabled = running;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveInput.action.Enable();
        ascendInput.action.Enable();
        descendInput.action.Enable();
        rotateInput.action.Enable();
    }

    void FixedUpdate()
    {
        // Ruch
        Vector2 move = moveInput.action.ReadValue<Vector2>();
        float ascend = ascendInput.action.ReadValue<float>();
        float descend = descendInput.action.ReadValue<float>();
        float verticalInput = ascend - descend;

        Vector3 horizontalMovement = transform.forward * move.y + transform.right * move.x;
        Vector3 verticalMovement = Vector3.up * verticalInput;
        Vector3 totalMovement = (horizontalMovement * moveSpeed) + (verticalMovement * ascendSpeed);
        rb.MovePosition(rb.position + totalMovement * Time.fixedDeltaTime);

        // Obrót
        Vector2 rotate = rotateInput.action.ReadValue<Vector2>();
        float yawInput = rotate.x;

        if (smoothRotation)
        {
            // płynny obrót
            float yaw = yawInput * rotateSpeed * Time.fixedDeltaTime;
            Quaternion rotation = Quaternion.Euler(0, yaw, 0);
            rb.MoveRotation(rb.rotation * rotation);
        }
        else
        {
            // skokowy obrót
            if (Time.time >= nextSnapTime)
            {
                if (yawInput > 0.5f)
                {
                    rb.MoveRotation(rb.rotation * Quaternion.Euler(0, snapAngle, 0));
                    nextSnapTime = Time.time + snapCooldown;
                }
                else if (yawInput < -0.5f)
                {
                    rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -snapAngle, 0));
                    nextSnapTime = Time.time + snapCooldown;
                }
            }
        }



        // Utrzymywanie drona w poziomie
        Quaternion desiredRotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);
        rb.rotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, 360 * Time.fixedDeltaTime);


        // Zatrzymywanie napędu (do poprawy)
        if (rb.position.y < 0.16)
        {
            UpdateRotors(false);
        }
        else UpdateRotors(true);


    }
}
