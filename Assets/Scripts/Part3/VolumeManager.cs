using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // Initialize sliders with current mixer values
        SetSliderValue(masterSlider, "MasterVolume");
        SetSliderValue(musicSlider, "AmbientVolume");
        SetSliderValue(sfxSlider, "SFXVolume");

        // Add listeners to sliders
        masterSlider.onValueChanged.AddListener(value => 
            SetVolume(value, "MasterVolume"));
        musicSlider.onValueChanged.AddListener(value => 
            SetVolume(value, "AmbientVolume"));
        sfxSlider.onValueChanged.AddListener(value => 
            SetVolume(value, "SFXVolume"));
    }

    private void SetSliderValue(Slider slider, 
        string paramName)
    {
        float value;
        audioMixer.GetFloat(paramName, out value);

        // For linear values use
        slider.value = value;

        // For logarithm values use
        // slider.value = Mathf.Pow(10, value / 20);
    }

    private void SetVolume(float sliderValue, 
        string paramName)
    {
        // For linear values use
        audioMixer.SetFloat(paramName, sliderValue);

        // For logarithm values use
        // float dbValue = Mathf.Log10(sliderValue) * 20;
        // audioMixer.SetFloat(paramName, dbValue);
    }
}