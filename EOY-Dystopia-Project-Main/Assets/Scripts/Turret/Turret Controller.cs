using System;
using System.Collections;
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
       
            _Controls.InputMap.CCTVCamera.TurretReload.performed += ctx => TryReload();

            _Controls.InputMap.CCTVCamera.TurretAim.performed += ctx => _isWeaponised = !_isWeaponised;
            
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
            if (_Controls.InputMap.CCTVCamera.TurretFire.ReadValue<float>() > 0 & _isWeaponised) TryShoot();
        
            #endregion

            if (isCoolingDown)
            {
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(_startRotation.x, _startRotation.y,0), Time.deltaTime * 1.5f);
            }

            #endregion
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
            _timeFromLastShot += Time.deltaTime;
            
            
            // Check if the player is trying to shoot
            if (GameManager.Instance.InputManager.InputMap.CCTVCamera.TurretFire.ReadValue<float>() > 0 && !outOfAmmo)
            {
                // Check if this is the players first shot before the cooldown or resting
                if (!_firstShot)
                {
                    _firstShot = true;
                }
                
                _timeFromLastShot = 0f;
            }
            
            // if the player hasn't shot for more than 0.2 seconds, set the first shot to false
            if (_timeFromLastShot > 0.2f)
            {
                _firstShot = false;
            }
            
            // if the player hasn't shot for more than the cooldown rest period, set the first shot to false and reset the time spent firing
            if (_timeFromLastShot > gunData.cooldownRestPeriod)
            {
                _firstShot = false;
                _timeFiring = 0f;
            }
            
            // if the player is currentlly firing, add to the time spent firing
            if (_firstShot)
            {
                _timeFiring += Time.deltaTime;
                
                // if the player has been firing for more than the cooldown time, start the cooldown
                if (_timeFiring > gunData.timeToCooldown && !isCoolingDown)
                {
                    StartCoroutine(CoolDown());
                }
            }

            //Debug.Log("First Shot:" + _firstShot);
            //Debug.Log("Time Firing:" + _timeFiring);
            //Debug.Log("Time From Last Shot:" + _timeFromLastShot);
            
        }

        // Coroutine to handle the cooldown of the turret
        private IEnumerator CoolDown()
        {
            Debug.Log("Cooldown Started");
            isCoolingDown = true;
            _isWeaponised = false;
            _timeFiring = 0f;
            yield return new WaitForSeconds(gunData.cooldownTime);
            transform.eulerAngles = _startRotation;
            _isWeaponised = true;
            isCoolingDown = false;
            Debug.Log("Cooldown Ended");
        }
    }
