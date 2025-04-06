using System;
using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
   public GunData gunData;
   
   private int _currentAmmo = 0;
   private float _nextTimeToFire = 0;

   protected bool IsFiring { get; set; }
   protected bool IsReloading { get; set; }
   protected bool OutOfAmmo { get; set; }
   

   
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
      HandleUI(); 
   }
   
   protected void TryReload()
   {
      if (IsReloading)
      {
         
         return;
      }
      
      StartCoroutine(Reload());
   }
   
   private IEnumerator Reload()
   {
      IsReloading = true;
      
      yield return new WaitForSeconds(gunData.reloadTime);
      
      _currentAmmo = gunData.magazineSize;
      
      IsReloading = false;
      
      OutOfAmmo = false;
      
   }

   
   protected void TryShoot()
   {
      if (Time.time < _nextTimeToFire)
      {
         return;
      }
      
      if (IsReloading)
      {
         
         Debug.Log(gunData.gunName + " is reloading");
         
         return;
      }
      
      if (_currentAmmo <= 0)
      {
         OutOfAmmo = true;
         return;
         
      }
      
      _nextTimeToFire = Time.time + (1f / gunData.fireRate);
      
      
      HandleShoot();
      
      
      
      
   }
   
   private void HandleUI()
   {
     
         if (!IsReloading && !OutOfAmmo)
         {
            GameManager.Instance.UIManager.AmmoCountUpdater(null, _currentAmmo, gunData.magazineSize);
         }
         if (OutOfAmmo)
         {
            GameManager.Instance.UIManager.AmmoCountUpdater("Out of Ammo", null, null);
         }
         if (IsReloading)
         {
            GameManager.Instance.UIManager.AmmoCountUpdater("Reloading...", null, null);
         }
      
      
   }

   private void HandleShoot()
   {
      _currentAmmo--;
      Shoot();
   }
   

   protected abstract void Shoot();

}
