using UnityEngine;

public class WindController : MonoBehaviour
{
    public float minChangeInterval = 5f;
    public float maxChangeInterval = 15f;

    public float minStrength = 0.5f;
    public float maxStrength = 2f;

    private Vector3 currentWind;
    private float timeToNextChange;

    void Start()
    {
        ChangeWind();
    }

    void Update()
    {
        timeToNextChange -= Time.deltaTime;

        if (timeToNextChange <= 0f)
        {
            ChangeWind();
        }
    }

    void ChangeWind()
    {
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.2f, 0.2f),
            Random.Range(-1f, 1f)
        ).normalized;

        float randomStrength = Random.Range(minStrength, maxStrength);

        currentWind = randomDirection * randomStrength;

        timeToNextChange = Random.Range(minChangeInterval, maxChangeInterval);
    }

    public Vector3 GetWind()
    {
        return currentWind;
    }
}
