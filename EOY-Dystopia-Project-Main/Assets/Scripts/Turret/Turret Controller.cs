using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

    public class TurretController : Gun
    {
        private InputManager _Controls;
       
        private Vector2 _cameraInput;
    
        [Header("CameraControls")]
        [SerializeField]
        private float _cameraMoveSpeed = .8f;
    
        [Header("GunControls")]
        [SerializeField]
        private GameObject firePoint;
    
        private bool _isWeaponised = false;

        [Header("Cooldown")]
        [SerializeField]
        private float _timeFromLastShot = 0f;
        private float _timeFiring = 0f;
        private bool _firstShot = false;
        private Vector3 _startRotation;
        
        public bool isCoolingDown { get; private set; }
  
    
        // Start is called before the first frame update

       

        public override void Start()
        {
            _Controls = GameManager.Instance.InputManager;
       
            Cursor.lockState = CursorLockMode.Locked;
            
            _startRotation = gameObject.transform.eulerAngles;
            
            
            base.Start();
        }

        private new void Update()
        {
            base.Update();
            #region Controls

            #region Movement Controls

            _cameraInput = _Controls.MovementInput;
        
            #endregion

            #region Turret Controls
            
            // Check if the player is trying to shoot
            if (_Controls.InputMap.CCTVCamera.TurretFire.ReadValue<float>() > 0 && _isWeaponised)
            {
                TryShoot();
            }
            else
            {
                IsFiring = false;
            }
            
            
            // Check if the player is trying to reload
            if (_Controls.InputMap.CCTVCamera.TurretReload.WasPerformedThisFrame()) TryReload();
            
            // Check if the player is trying to aim
            if (_Controls.InputMap.CCTVCamera.TurretAim.WasPerformedThisFrame()) _isWeaponised = !_isWeaponised;
            #endregion

            #region Scene

            if (GameManager.Instance.InputManager.InputMap.CCTVCamera.Exit.triggered)
            {
                GameManager.Instance.SceneController.InsideScene();
            }

            #endregion
            
            #endregion
            
            if (isCoolingDown)
            {
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(_startRotation.x, _startRotation.y,0), Time.deltaTime * 1.5f);
            }
            
            if (_Controls.InputMap.CCTVCamera.TurretFire.ReadValue<float>() > 0 && _isWeaponised && !isCoolingDown && !IsReloading && !OutOfAmmo)
            {
                IsFiring = true;
            }
            else
            {
                IsFiring = false;
            }
                
            UiHandler();
        }

        private void LateUpdate()
        {
            #region Camera Movement
        
            if (_isWeaponised && !isCoolingDown)
            {
                // Get the input from the input manager
                _cameraInput = GameManager.Instance.InputManager.LookInput;
                CameraMovement();
            }
            #endregion
        }

        private void FixedUpdate()
        {
            if (!_isWeaponised || !isCoolingDown) CameraMovement();
            
            CoolDownSystem();
        }
        
        private void CameraMovement()
        {
            // Rotate the camera based on the input
            float currentY = gameObject.transform.localEulerAngles.y;
            float newRotationY = Mathf.Clamp((currentY > 180  ? currentY - 360 : currentY) + _cameraInput.x * _cameraMoveSpeed, -90, 90);
            
            // Rotate the camera based on the input
            float currentX = gameObject.transform.localEulerAngles.x;
            float newRotationX = Mathf.Clamp((currentX > 180  ? currentX - 360 : currentX) + _cameraInput.y * _cameraMoveSpeed, -90f, 90f);

            gameObject.transform.localEulerAngles = new Vector3(newRotationX, newRotationY, gameObject.transform.localEulerAngles.z);
        }
        
        protected override void Shoot()
        {
            Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out var hit, gunData.range, LayerMask.GetMask("Enemy"));
            Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * gunData.range, Color.red);
            
            // Check if the raycast hit an enemy
            if (hit.collider is not null)
            {
                
                // If the raycast hit an enemy, deal damage to the enemy
                hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(gunData.damage);
            
                Debug.Log(gunData.gunName + " Shot " + hit.transform.gameObject.name +  " for " + gunData.damage + " damage");
            }
            else
            {
                Debug.Log(gunData.gunName + " Missed");
            }
        }

        private void CoolDownSystem()
        {
            // Check if the player is trying to shoot
            if (IsFiring)
            {
                // Check if this is the players first shot before the cooldown or resting
                if (!_firstShot)
                {
                    _firstShot = true;
                }
                
                _timeFromLastShot = 0f;
            }
            else
            {
                _timeFromLastShot += Time.deltaTime;
                _firstShot = false;
            }
            
            // if the player hasn't shot for more than the cooldown rest period, set the first shot to false and reset the time spent firing
            if (_timeFromLastShot > gunData.cooldownRestPeriod)
            {
                _firstShot = false;
                _timeFiring = Mathf.Lerp(_timeFiring, 0, Time.deltaTime * _timeFromLastShot * 1.5f);
            }
            
            // if the player is currentlly firing, add to the time spent firing
            if (_firstShot)
            {
                _timeFiring += Time.deltaTime;
                
                // if the player has been firing for more than the cooldown time, start the cooldown
                if (_timeFiring > gunData.timeFiringUntilCooldown && !isCoolingDown)
                {
                    StartCoroutine(CoolDown());
                }
            }
            
        }
        
        // Coroutine to handle the cooldown of the turret
        private IEnumerator CoolDown()
        {
            Debug.Log("Cooldown Started");
            DisableTurret();
            yield return new WaitForSeconds(gunData.cooldownTime);
            EnableTurret();
            Debug.Log("Cooldown Ended");
        }

        private void DisableTurret()
        {
            isCoolingDown = true;
            _isWeaponised = false;
            _firstShot = false;
        }

        private void EnableTurret()
        {
            transform.eulerAngles = _startRotation;
            _isWeaponised = true;
            isCoolingDown = false;
        }
        
        private void UiHandler()
        {
            GameManager.Instance.UIManager.TurretCooldownUpdater(_timeFiring / gunData.timeFiringUntilCooldown);
        }
    }
