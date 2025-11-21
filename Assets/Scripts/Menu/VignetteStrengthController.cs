using UnityEngine;
using UnityEngine.UI;

// Controls the strength of the vignette effect based on a UI slider.
// Syncs the slider with PlayerPrefs and updates the target vignette aperture.
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

        // Convert slider value (0-1) to vignette aperture (0.9–0.6)
        float vignetteStrength = (0.9f - (sliderValue * 0.3f));
        PlayerPrefs.SetFloat("vignetteStrength", vignetteStrength);

        UpdateVignetteStrength(vignetteStrength);
    }

    public void UpdateVignetteStrength(float vignetteStrength)
    {
        vignetteController.targetAperture = vignetteStrength;
    }
}
