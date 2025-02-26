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
    
    [Header("reloadConfig")]
    public float magazineSize;
    public float reloadTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
