using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Config")]
    [SerializeField] private AudioSource UIAudioSource;
    [SerializeField] private AudioSource GunShotAudioSource;
    [SerializeField] private AudioSource TerminalAudioSource;
    
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
    
    
}
