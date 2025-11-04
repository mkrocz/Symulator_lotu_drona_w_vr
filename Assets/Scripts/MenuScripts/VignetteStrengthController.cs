using UnityEngine;
using UnityEngine.UI;

public class VignetteStrengthController : MonoBehaviour
{
    public VignetteController vignetteController;

    private string playerPrefKey = "vignetteStrength";
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (slider != null)
        {
            float savedValue = PlayerPrefs.GetFloat(playerPrefKey, 0f);
            slider.value = savedValue;
            slider.onValueChanged.AddListener(OnValueChanged);
        }
    }

    public void OnValueChanged(float sliderValue)
    {
        PlayerPrefs.SetFloat(playerPrefKey, sliderValue);
        PlayerPrefs.Save();

        float vignetteStrength = sliderValue = 0.6f + (sliderValue * 0.3f);

        UpdateVignetteStrength(vignetteStrength);
    }

    public void UpdateVignetteStrength(float vignetteStrength)
    {
        vignetteController.targetAperture = vignetteStrength;
    }
}
