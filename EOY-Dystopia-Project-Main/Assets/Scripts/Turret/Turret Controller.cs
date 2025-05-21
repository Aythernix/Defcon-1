using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Unity.VisualScripting;

public class TurretController : Gun
{
    private InputManager _Controls;
    private Vector2 _cameraInput;

    [Header("Camera Controls")]
    [SerializeField] private float _cameraMoveSpeed = 0.8f;

    [Header("Gun Controls")]
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletSpawnPoint;

    public bool isActive;
    public bool isActiveTurret;

    [Header("Cooldown")]
    [SerializeField] private float _timeFromLastShot = 0f;
    private float _timeFiring = 0f;
    private bool _firstShot = false;
    private Vector3 _startRotation;

    private bool _isCoolingDown;
    

    public override void Start()
    {
        _Controls = GameManager.Instance.InputManager;
        Cursor.lockState = CursorLockMode.Locked;
        _startRotation = transform.eulerAngles;

        base.Start();
        
        isActive = GameManager.Instance.PowerSystem.isPowerActive;
    }
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnPowerChange += PowerStateChange;
    }

    private void PowerStateChange(bool state)
    {
        isActive = state;
    }

    private new void Update()
    {
        base.Update();

        #region Controls

        #region Turret Controls

        if (_Controls.InputMap.CCTVCamera.TurretFire.ReadValue<float>() > 0 && isActive && !_isCoolingDown)
        {
            TryShoot();
        }
        else
        {
            IsFiring = false;
        }

        if (_Controls.InputMap.CCTVCamera.TurretReload.WasPerformedThisFrame() && isActive && !_isCoolingDown)
        {
            TryReload();
        }

        #endregion

        #region Scene Transition

        if (_Controls.InputMap.CCTVCamera.Exit.triggered && isActiveTurret)
        {
            StartCoroutine(GameManager.Instance.SceneController.InsideScene());
        }

        #endregion

        #endregion
        
        
        if (_Controls.InputMap.CCTVCamera.TurretFire.ReadValue<float>() > 0 &&
            isActive && !_isCoolingDown && !IsReloading && !OutOfAmmo)
        {
            IsFiring = true;
        }
        else
        {
            IsFiring = false;
        }

        if (_isCoolingDown)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(_startRotation.x, _startRotation.y, 0),
                Time.deltaTime * 1.5f
            );
        }

        

        UiHandler();
    }

    private void LateUpdate()
    {
        if (isActive && !_isCoolingDown)
        {
            _cameraInput = _Controls.LookInput;
            CameraMovement();
        }
    }

    private void FixedUpdate()
    {
        if (!isActive || !_isCoolingDown)
        {
            CameraMovement();
        }

        CoolDownSystem();
    }

    private void CameraMovement()
    {
        float currentY = transform.localEulerAngles.y;
        float newRotationY = Mathf.Clamp(
            (currentY > 180 ? currentY - 360 : currentY) + _cameraInput.x * _cameraMoveSpeed,
            -90, 90
        );

        float currentX = transform.localEulerAngles.x;
        float newRotationX = Mathf.Clamp(
            (currentX > 180 ? currentX - 360 : currentX) + _cameraInput.y * _cameraMoveSpeed,
            -90f, 90f
        );

        transform.localEulerAngles = new Vector3(newRotationX, newRotationY, transform.localEulerAngles.z);
    }

    protected override void Shoot()
    {
        Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out var hit, gunData.range, LayerMask.GetMask("Enemy"));
        GetComponent<CameraShake>().shakeDuration = 0.1f;
        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * gunData.range, Color.red);

        Instantiate(gunData.bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);

        if (hit.collider != null)
        {
            hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(gunData.damage);
            Debug.Log($"{gunData.gunName} Shot {hit.transform.gameObject.name} for {gunData.damage} damage");
        }
        else
        {
            
        }
    }

    private void CoolDownSystem()
    {
        if (IsFiring)
        {
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

        if (_timeFromLastShot > gunData.cooldownRestPeriod)
        {
            _firstShot = false;
            _timeFiring = Mathf.Lerp(_timeFiring, 0, Time.deltaTime * _timeFromLastShot * 1.5f);
        }

        if (_firstShot)
        {
            _timeFiring += Time.deltaTime;

            if (_timeFiring > gunData.timeFiringUntilCooldown && !_isCoolingDown)
            {
                StartCoroutine(CoolDown());
            }
        }
    }

    private IEnumerator CoolDown()
    {
        Debug.Log("Cooldown Started");
        GameManager.Instance.EventManager.TurretCooldown(true);
        _isCoolingDown = true;
        _firstShot = false;
        yield return new WaitForSeconds(gunData.cooldownTime);
        GameManager.Instance.EventManager.TurretCooldown(false);
        _isCoolingDown = false;
        Debug.Log("Cooldown Ended");
    }

    private void DisableTurret()
    {
        isActive = false;
    }

    private void EnableTurret()
    {
        isActive = true;
    }

    private void UiHandler()
    {
        if (!isActiveTurret || !isActive) return;

        GameManager.Instance.UIManager.TurretCooldownUpdater(_timeFiring / gunData.timeFiringUntilCooldown);

        if (!IsReloading && !OutOfAmmo)
        {
            GameManager.Instance.UIManager.AmmoCountUpdater(null, currentAmmo, gunData.magazineSize);
        }
        else if (OutOfAmmo && !IsReloading)
        {
            GameManager.Instance.UIManager.AmmoCountUpdater("Out of Ammo", null, null);
        }
        else if (IsReloading)
        {
            GameManager.Instance.UIManager.AmmoCountUpdater("Reloading...", null, null);
        }
    }
}
