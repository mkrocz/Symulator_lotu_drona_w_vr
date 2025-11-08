using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class WindSetting : MonoBehaviour
{
    private string playerPrefKey = "Wind";
    private Toggle toggle;

    public DroneMovement droneMovement;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    void OnEnable()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(OnToggleChanged);

            bool savedState = PlayerPrefs.GetInt(playerPrefKey, 0) == 1;
            toggle.isOn = savedState;
        }
    }

    void OnDisable()
    {
        if (toggle != null)
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt(playerPrefKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateWind(isOn);
    }

    private void UpdateWind(bool isOn)
    {
        droneMovement.SetWind(isOn);
    }

}
