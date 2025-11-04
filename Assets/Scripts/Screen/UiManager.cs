using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public DroneInfo droneInfo;
    public TMP_Text uiText;

    void Update()
    {
        Vector3 pos = droneInfo.GetPosition();
        Vector3 vel = droneInfo.GetVelocity();
        float speed = droneInfo.GetSpeed();

        uiText.text = $"Position: {pos}\nVelocity: {vel}\nSpeed: {speed}";
    }
}
