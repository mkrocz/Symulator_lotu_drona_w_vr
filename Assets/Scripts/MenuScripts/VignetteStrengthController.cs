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
            float sliderValue = PlayerPrefs.GetFloat("vignetteStrengthSlider", 0f);
            slider.value = sliderValue;
            slider.onValueChanged.AddListener(OnValueChanged);
        }
        float vignetteStrength = PlayerPrefs.GetFloat("vignetteStrength", 0f);
        UpdateVignetteStrength(vignetteStrength);
    }

    public void OnValueChanged(float sliderValue)
    {
        PlayerPrefs.SetFloat("vignetteStrengthSlider", sliderValue);
        PlayerPrefs.Save();

        float vignetteStrength = (0.9f - (sliderValue * 0.3f));
        PlayerPrefs.SetFloat("vignetteStrength", vignetteStrength);

        UpdateVignetteStrength(vignetteStrength);
    }

    public void UpdateVignetteStrength(float vignetteStrength)
    {
        vignetteController.targetAperture = vignetteStrength;
    }
}
