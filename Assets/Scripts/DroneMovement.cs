using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneMovement : MonoBehaviour
{
    public float ascendSpeed = 1.5f;
    public float moveSpeed = 2f;
    public float rotateSpeed = 90f;

    private Rigidbody rb;
    public Animator[] rotors;

    private Vector3 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Potrzebne do obliczenia predkosci
        //lastPosition = rb.position;
    }

    public void Move(Vector3 inputMovement, float inputAscend, float inputYaw)
    {
        // Ruch poziomy
        Vector3 horizontalMovement = transform.forward * inputMovement.z + transform.right * inputMovement.x;

        // Ruch pionowy 
        Vector3 verticalMovement = Vector3.up * inputAscend;

        // Suma
        Vector3 totalMovement = (horizontalMovement * moveSpeed) + (verticalMovement * ascendSpeed);
        rb.MovePosition(rb.position + totalMovement * Time.fixedDeltaTime);

        // Obrot
        float yaw = inputYaw * rotateSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, yaw, 0));

        // Utrzymanie poziomu
        Quaternion desiredRotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);
        rb.rotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, 360 * Time.fixedDeltaTime);

        // Rotory (rozwiazanie tymczasowe)
        bool running = rb.position.y >= 0.16f;
        foreach (var rotor in rotors)
        {
            rotor.enabled = running;
        }



        // Obliczanie predkosci (debug)
        /*
        Vector3 velocity = (rb.position - lastPosition) / Time.fixedDeltaTime;
        float horizontalSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;
        float verticalSpeed = velocity.y;
        float totalSpeed = velocity.magnitude;

        Debug.Log($"Predkość pozioma: {horizontalSpeed:F2}, pionowa: {verticalSpeed:F2}, calkowita: {totalSpeed:F2}");

        lastPosition = rb.position;
        */

    }
}
