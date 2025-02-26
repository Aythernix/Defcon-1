using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
   public GunData gunData;
   
   private float _currentAmmo = 0f;
   private float _nextTimeToFire = 0f;
   
   private bool isReloding = false;

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
      if (isReloding)
      {
         Debug.Log(gunData.gunName + " is already reloading");
         return;
      }
      
      StartCoroutine(Reload());
   }
   
   private IEnumerator Reload()
   {
      isReloding = true;
      
      yield return new WaitForSeconds(gunData.reloadTime);
      
      _currentAmmo = gunData.magazineSize;
      
      isReloding = false;
      
      Debug.Log(gunData.gunName + " Reloaded");
      
      
   }

   protected void TryShoot()
   {
      if (Time.time < _nextTimeToFire)
      {
         return;
      }
      
      if (isReloding)
      {
         Debug.Log(gunData.gunName + " is reloading");
         
         return;
      }
      
      if (_currentAmmo <= 0)
      {
         
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
