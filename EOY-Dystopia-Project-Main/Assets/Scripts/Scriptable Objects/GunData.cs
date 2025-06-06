using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewGunData", menuName = "Weapons/GunData")]
public class GunData : ScriptableObject
{
    public string gunName;
    
    [Header("FireConfig")]
    public float fireRate;
    public float range;
    public float damage;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    
    [Header("CooldownConfig")]
    public float cooldownTime;
    public float timeFiringUntilCooldown;
    public float cooldownRestPeriod;
    
    [Header("reloadConfig")]
    public int magazineSize;
    public float reloadTime;
    
    [Header("WeaponConfig")]
    public GameObject gunModel;
    public GameObject bulletPrefab;
    
}
