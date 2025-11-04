using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class VignetteController : MonoBehaviour
{
    [Header("Referencje")]
    public Transform drone;

    [Header("Parametry winiety")]
    [Range(0f, 1f)] public float targetAperture;
    [Range(0f, 1f)] public float targetFeather;

    [Header("P³ynnoœæ przejœcia")]
    public float smoothSpeed = 5f;

    [Header("Sterowanie ruchem drona")]
    public float movementThreshold = 0.05f;
    public float stopDelay = 0.2f;

    private Material matInstance;

    private float currentAperture;
    private float currentFeather;

    private bool isDroneMoving;

    private Vector3 lastPos;
    private float stopTimer = 0f;

    void Awake()
    {
        matInstance = GetComponent<Renderer>().material;

        currentAperture = 1f;
        currentFeather = 1f;

        if (drone != null)
            lastPos = drone.position;
    }

    void Update()
    {
        float speed = (drone.position - lastPos).magnitude / Time.deltaTime;

        if (speed > movementThreshold)
        {
            isDroneMoving = true;
            stopTimer = 0f;
        }
        else
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopDelay)
                isDroneMoving = false;
        }

        lastPos = drone.position;

        if (isDroneMoving)
        {
            currentAperture = Mathf.MoveTowards(currentAperture, targetAperture, smoothSpeed * Time.deltaTime);
            currentFeather = Mathf.MoveTowards(currentFeather, targetFeather, smoothSpeed * Time.deltaTime);

            matInstance.SetFloat("_ApertureSize", currentAperture);
            matInstance.SetFloat("_FeatheringEffect", currentFeather);
        }
        else
        {
            currentAperture = Mathf.MoveTowards(currentAperture, 1, smoothSpeed * Time.deltaTime);
            currentFeather = Mathf.MoveTowards(currentFeather, 1, smoothSpeed * Time.deltaTime);

            matInstance.SetFloat("_ApertureSize", currentAperture);
            matInstance.SetFloat("_FeatheringEffect", currentFeather);
        }
    }

}
