using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/SettingsData")]
public class SettingsData : ScriptableObject
{
    [Header("Sound Settings")]
    public float masterVolume = 1f;
    public float gameplayVolume = 1f;
    public float effectsVolume = 1f;
    
    [Header("Gameplay Settings")]
    public float sensitivity = 1f;
    
  
}
