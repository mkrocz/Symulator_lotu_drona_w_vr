using UnityEngine;

// Rotates the object so that it always faces the target camera horizontally.
// The camera should have the "MainCamera" tag to be assigned automatically.
public class Billboard : MonoBehaviour
{
    public Camera targetCamera;

    void OnEnable()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 camPos = targetCamera.transform.position;
        camPos.y = transform.position.y;
        transform.LookAt(2 * transform.position - camPos);

    }
}
