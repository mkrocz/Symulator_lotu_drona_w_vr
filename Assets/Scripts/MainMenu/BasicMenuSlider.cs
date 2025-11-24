using UnityEngine;
using UnityEngine.UI;

// Controls the strength of the vignette effect based on a UI slider.
// Syncs the slider with PlayerPrefs and updates the target vignette aperture.
public class BasicMenuSlider : MonoBehaviour
{
    public string playerPrefKey;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (slider != null)
        {
            float sliderValue = PlayerPrefs.GetFloat(playerPrefKey, 0f);
            slider.value = sliderValue;
            slider.onValueChanged.AddListener(OnValueChanged);
        }
    }

    private void OnDisable()
    {
        if (slider != null)
            slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void OnValueChanged(float sliderValue)
    {
        PlayerPrefs.SetFloat(playerPrefKey, sliderValue);
        PlayerPrefs.Save();
    }

}
