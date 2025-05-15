using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Sliders")]
    [SerializeField] private GameObject _masterVolumeSlider;
    [SerializeField] private GameObject _gameplayVolumeSlider;
    [SerializeField] private GameObject _effectsVolumeSlider;
    [SerializeField] private GameObject _uiVolumeSlider;
    [SerializeField] private GameObject _sensitivitySlider;

    // Start is called before the first frame update
    void Start()
    {
        // Set the sliders to the current volume levels
        _masterVolumeSlider.GetComponentInChildren<Slider>().value = GameManager.Instance.AudioManager.GetVolume("MasterVolume");
        _gameplayVolumeSlider.GetComponentInChildren<Slider>().value = GameManager.Instance.AudioManager.GetVolume("GameplayVolume");
        _effectsVolumeSlider.GetComponentInChildren<Slider>().value = GameManager.Instance.AudioManager.GetVolume("EffectsVolume");
        _uiVolumeSlider.GetComponentInChildren<Slider>().value = GameManager.Instance.AudioManager.GetVolume("UIVolume");
        
        // Set the sensitivity slider to the current camera sensitivity
        _sensitivitySlider.GetComponentInChildren<Slider>().value = GameManager.Instance.InputManager.cameraSensitivity;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMasterVolume(float sliderValue)
    {
        GameManager.Instance.AudioManager.SetVolume(sliderValue, "MasterVolume");
    }
    
    public void SetGameplayVolume(float sliderValue)
    {
        GameManager.Instance.AudioManager.SetVolume(sliderValue, "GameplayVolume");
    }
    
    public void SetEffectVolume(float sliderValue)
    {
        GameManager.Instance.AudioManager.SetVolume(sliderValue, "EffectsVolume");
    }
    
    public void SetUIVolume(float sliderValue)
    {
        GameManager.Instance.AudioManager.SetVolume(sliderValue, "UIVolume");
    }
    
    public void SetSensitivity(float sliderValue)
    {
        GameManager.Instance.InputManager.cameraSensitivity = sliderValue;
    }
    
    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
