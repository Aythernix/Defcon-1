using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGunData", menuName = "Weapons/GunData")]
public class GunData : ScriptableObject
{
    public string gunName;
    
    [Header("FireConfig")]
    public float fireRate;
    public float range;
    public float damage;
    
    [Header("reloadConfig")]
    public float magazineSize;
    public float reloadTime;
    
    public GameObject bulletPrefab;
}
