using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneMovement : MonoBehaviour
{
    // Movement settings
    public float ascendSpeed = 1.5f;
    public float moveSpeed = 2f;
    public float rotateSpeed = 90f;
    public float snapAngle = 45f;
    public bool snapRotation = false;
    public float snapCooldown = 0.3f;
    private float nextSnapTime = 0f;

    private Rigidbody rb;


    // Drone tilting
    private bool simpleMovement;
    public Transform droneModel;
    public float tiltAmount = 15f;
    public float tiltSpeed = 5f;

    // Wind influence
    bool isWindActive;
    public WindController wind;
    public float windInfluence = 0.5f;


    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        simpleMovement = PlayerPrefs.GetInt("SimpleMovement", 0) == 1;
        isWindActive = PlayerPrefs.GetInt("Wind", 0) == 1;
    }

    // Moves the drone object based on input.
    // Handles horizontal and vertical movement, rotation,
    // tilting and wind influence.
    public void Move(Vector3 inputMovement, float inputAscend, float inputYaw)
    {
        Vector3 horizontalDir = transform.forward * inputMovement.z + transform.right * inputMovement.x;
        Vector3 targetVelocity = horizontalDir * moveSpeed + Vector3.up * inputAscend * ascendSpeed;

        rb.linearVelocity = targetVelocity;

        if (!snapRotation)
        {
            float yaw = inputYaw * rotateSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, yaw, 0));
        }
        else
        {
            if (Time.time >= nextSnapTime)
            {
                if (inputYaw > 0.5f)
                {
                    rb.MoveRotation(rb.rotation * Quaternion.Euler(0, snapAngle, 0));
                    nextSnapTime = Time.time + snapCooldown;
                }
                else if (inputYaw < -0.5f)
                {
                    rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -snapAngle, 0));
                    nextSnapTime = Time.time + snapCooldown;
                }
            }
        }

        // Maintaining level
        Quaternion desiredRotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);
        rb.rotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, 360 * Time.fixedDeltaTime);



        if (!simpleMovement)
            DroneTilting(rb.linearVelocity);

        if (isWindActive)
            WindInfluence(rb);

    }


    // Handles drone visual tilting, separate from the main movement logic.
    private void DroneTilting(Vector3 totalMovement)
    {
        if (droneModel != null)
        {
            Vector3 worldVelocityEstimate = totalMovement;
            Vector3 localVelocity = transform.InverseTransformDirection(worldVelocityEstimate);

            float tiltForward = Mathf.Clamp(localVelocity.z * tiltAmount, -tiltAmount, tiltAmount);
            float tiltSide = Mathf.Clamp(-localVelocity.x * tiltAmount, -tiltAmount, tiltAmount);

            Quaternion targetTilt = Quaternion.Euler(tiltForward, 0, tiltSide);
            droneModel.localRotation = Quaternion.Slerp(droneModel.localRotation, targetTilt, Time.fixedDeltaTime * tiltSpeed);
        }
    }

    // Applies the current wind force from the WindController to the drone,
    // scaled by windInfluence and adjusted for FixedUpdate deltaTime.
    private void WindInfluence(Rigidbody rb)
    {
        if (wind != null && windInfluence > 0f)
        {
            Vector3 windForce = wind.GetWind() * windInfluence;
            rb.MovePosition(rb.position + windForce * Time.fixedDeltaTime);
        }
    }

    public void SetSimpleMovement(bool simpleMovement)
    {
        this.simpleMovement = simpleMovement;
    }

    public void SetWind(bool wind)
    {
        this.isWindActive = wind;
    }

}
