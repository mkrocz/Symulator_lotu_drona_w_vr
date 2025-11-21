using UnityEngine;

// Provides information about drone's posiotion and speed.
public class DroneInfo : MonoBehaviour
{
    public Rigidbody rb;

    private Vector3 lastPosition;
    private Vector3 velocity;
    private float horizontalSpeed;
    private float verticalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = rb.position; // needed for velocity calculations
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition = rb.position;

        velocity = (currentPosition - lastPosition) / Time.fixedDeltaTime;

        Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);
        horizontalSpeed = horizontalVelocity.magnitude;
        verticalSpeed = Mathf.Abs(velocity.y);

        lastPosition = currentPosition;
    }


    public Vector3 GetPosition()
    {
        return rb.position;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public float GetHorizontalSpeed()
    {
        return horizontalSpeed;
    }

    public float GetVerticalSpeed()
    {
        return verticalSpeed;
    }
}
