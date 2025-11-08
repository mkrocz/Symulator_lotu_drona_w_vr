using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneMovement : MonoBehaviour
{
    public float ascendSpeed = 1.5f;
    public float moveSpeed = 2f;
    public float rotateSpeed = 90f;
    public float snapAngle = 45f;
    public bool snapRotation = false;
    public float snapCooldown = 0.3f;
    private float nextSnapTime = 0f;

    private Rigidbody rb;


    // Realistyczny ruch drona
    private bool simpleMovement;
    public Transform droneModel;
    public float tiltAmount = 15f;
    public float tiltSpeed = 5f;

    // Wpływ wiatru
    bool isWindActive;
    public WindController wind;
    public float windInfluence = 0.5f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        simpleMovement = PlayerPrefs.GetInt("SimpleMovement", 0) == 1;
        isWindActive = PlayerPrefs.GetInt("Wind", 0) == 1;
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

        // Obrot (implementacja bez skokowego obrotu)
        /*
        float yaw = inputYaw * rotateSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, yaw, 0));
        */


        // Obrot (plynny/skokowy obrot)
        if (!snapRotation)
        {
            // plynny obrot
            float yaw = inputYaw * rotateSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, yaw, 0));
        }
        else
        {
            // skokowy obrót
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
        // Utrzymanie poziomu
        Quaternion desiredRotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);
        rb.rotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, 360 * Time.fixedDeltaTime);

        if (!simpleMovement)
            DroneTilting(totalMovement);

        if (isWindActive)
            WindInfluence(rb);

    }


    private void DroneTilting(Vector3 totalMovement)
    {
        // Przechylanie drona przy poruszaniu
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
