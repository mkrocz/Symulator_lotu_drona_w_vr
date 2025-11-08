using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public DroneInfo droneInfo;
    public WindController windController;
    public TMP_Text uiText;

    void Update()
    {
        Vector3 pos = droneInfo.GetPosition();
        Vector3 vel = droneInfo.GetVelocity();
        float vSpeed = droneInfo.GetVerticalSpeed();
        float hSpeed = droneInfo.GetHorizontalSpeed();
        Vector3 wind = windController.GetWind();

        uiText.text = $"Pozycja: {pos}\nPrêdkoœæ pozioma: {vSpeed}\nPrêdkoœæ pionowa: {hSpeed}\nWiatr: {wind}";
    }
}
