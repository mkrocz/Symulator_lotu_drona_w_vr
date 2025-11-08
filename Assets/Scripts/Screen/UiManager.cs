using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public DroneInfo droneInfo;
    public WindController windController;
    public TMP_Text uiText;
    private bool isWindActive;

    private void Start()
    {
        isWindActive = PlayerPrefs.GetInt("Wind", 0) == 1;
    }

    void Update()
    {
        Vector3 pos = droneInfo.GetPosition();
        Vector3 vel = droneInfo.GetVelocity();
        float vSpeed = droneInfo.GetVerticalSpeed();
        float hSpeed = droneInfo.GetHorizontalSpeed();
        Vector3 wind = windController.GetWind();

        uiText.text = $"Pozycja: {pos}\nPrêdkoœæ pozioma: {hSpeed:F1} m/s\nPrêdkoœæ pionowa: {vSpeed:F1} m/s";

        /*
        if (isWindActive)
        {
            uiText.text += $"\nWiatr: {wind}";
        }
        */

        if (isWindActive)
        {
            float windSpeed = wind.magnitude;
            uiText.text += $"\nWektor wiatru: {wind}";
            uiText.text += $"\nPrêdkoœæ wiatru: {windSpeed:F1} m/s)";
        }
    }

    public void SetIsWindActive(bool state)
    {
        this.isWindActive = state;
    }
}
