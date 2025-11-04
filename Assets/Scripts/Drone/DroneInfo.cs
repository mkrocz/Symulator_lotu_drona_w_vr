using UnityEngine;

public class DroneInfo : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 lastPosition;
    private Vector3 velocity;
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = rb.position;
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition = rb.position;
        speed = Vector3.Distance(currentPosition, lastPosition) / Time.fixedDeltaTime;
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

    public float GetSpeed()
    {
        return speed;
    }
}
