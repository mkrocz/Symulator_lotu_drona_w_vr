using UnityEngine;
using UnityEngine.UI;

// Controls the strength of the vignette effect based on a UI slider.
// Syncs the slider with PlayerPrefs and updates the target vignette aperture.
public class VignetteStrengthController : MonoBehaviour
{
    public VignetteController vignetteController;

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
            OnStartup(sliderValue);
        }
       
    }

    private void OnDisable()
    {
        if (slider != null)
            slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void OnValueChanged(float sliderValue)
    {
        PlayerPrefs.SetFloat("vignetteStrengthSlider", sliderValue);
        PlayerPrefs.Save();

        float vignetteStrength = SliderToVignette(sliderValue);
        PlayerPrefs.SetFloat("vignetteStrength", vignetteStrength);

        UpdateVignetteStrength(vignetteStrength);
    }

    public void UpdateVignetteStrength(float vignetteStrength)
    {
        vignetteController.targetAperture = vignetteStrength;
    }

    private void OnStartup(float sliderValue)
    {
        float vignetteStrength = SliderToVignette(sliderValue);
        PlayerPrefs.SetFloat("vignetteStrength", vignetteStrength);
        UpdateVignetteStrength(vignetteStrength);

    }

    private float SliderToVignette(float sliderValue)
    {
        // Convert slider value (0-1) to vignette aperture (0.9–0.6)
        return 0.9f - (sliderValue * 0.3f);
    }

}
