using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    
    [Header("Audio Config")]
    [SerializeField] private AudioSource UIAudioSource;
    [SerializeField] private AudioSource GunShotAudioSource;
    [SerializeField] private AudioSource TerminalAudioSource;
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("Audio Clips")]
    public AudioClip UIClick;
    public AudioClip UIHover;
    public AudioClip TerminalEnter;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayUIAudio(AudioClip clip)
    {
        UIAudioSource.clip = clip;
        UIAudioSource.Play();
    }

    public void PlayGunShot(AudioClip gunShot)
    {
       GunShotAudioSource.clip = gunShot;
       GunShotAudioSource.Play();
    }
    
    public void PlayTerminalSounds(AudioClip clip)
    {
        TerminalAudioSource.clip = clip;
        TerminalAudioSource.Play();
    }
    
    public void SetVolume(float sliderValue, string mix)
    {
        audioMixer.SetFloat(mix, Mathf.Log10(sliderValue) * 20);
    }
    public float GetVolume(string mix)
    {
        audioMixer.GetFloat(mix, out float value);
        
        return Mathf.Pow(10, value / 20);
    }
    
    
}
