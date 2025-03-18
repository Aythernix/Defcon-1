using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
   public GunData gunData;
   
   private float _currentAmmo = 0f;
   private float _nextTimeToFire = 0f;
   
   private bool _isReloading = false;
   public bool outOfAmmo { get; private set; }

   
   public virtual void Awake()
   {
      // set the mesh and model to the mesh in gun data
      gameObject.GetComponentInChildren<MeshFilter>().mesh = gunData.gunModel.GetComponent<MeshFilter>().sharedMesh;
      gameObject.GetComponentInChildren<MeshRenderer>().materials = gunData.gunModel.GetComponent<MeshRenderer>().sharedMaterials;
   }
   public virtual void Start()
   {
      _currentAmmo = gunData.magazineSize;

      Debug.Log(gunData.gunName + " Loaded, Ammo: " + _currentAmmo);
   }

   public virtual void Update()
   {
      
   }
   
   protected void TryReload()
   {
      if (_isReloading)
      {
         Debug.Log(gunData.gunName + " is already reloading");
         return;
      }
      
      StartCoroutine(Reload());
   }
   
   private IEnumerator Reload()
   {
      _isReloading = true;
      
      Debug.Log(gunData.gunName + " is reloading");
      
      yield return new WaitForSeconds(gunData.reloadTime);
      
      _currentAmmo = gunData.magazineSize;
      
      _isReloading = false;
      
      Debug.Log(gunData.gunName + " Reloaded");
      
      
   }

   protected void TryShoot()
   {
      if (Time.time < _nextTimeToFire)
      {
         return;
      }
      
      if (_isReloading)
      {
         Debug.Log(gunData.gunName + " is reloading");
         
         return;
      }
      
      if (_currentAmmo <= 0)
      {
         outOfAmmo = true;
         Debug.Log(gunData.gunName + " is out of ammo");
         return;
         
      }
      
      _nextTimeToFire = Time.time + (1f / gunData.fireRate);
      
      HandleShoot();
      
      
   }

   private void HandleShoot()
   {
      _currentAmmo--;
      
      Debug.Log(gunData.gunName + " Shot, Ammo: " + _currentAmmo);     
      Shoot();
   }

   protected abstract void Shoot();

}
