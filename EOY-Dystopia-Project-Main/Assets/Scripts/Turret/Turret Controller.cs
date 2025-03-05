using UnityEngine;


public class TurretController : Gun
{
    private InputMap.CCTVCameraActions _Controls;
       
    private Vector2 _cameraInput;
    
    [Header("CameraControls")]
    
    [SerializeField]
    private float _cameraMoveSpeed = .8f;
    
    [Header("GunControls")]
    
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private float _Sensitivity = 100f;
    
    private bool _isWeaponised = false;
    
  
    
    // Start is called before the first frame update
    public override void Start()
    {
       _Controls = GameManager.Instance.InputManager.InputMap.CCTVCamera;
       Debug.Log("ran");
       
       Cursor.lockState = CursorLockMode.Locked;
       
       _Controls.TurretReload.performed += ctx => TryReload();

       _Controls.TurretAim.performed += ctx => _isWeaponised = !_isWeaponised;
       
       base.Start();
    }

    private new void Update()
    {
        base.Update();
        
        #region Controls

        #region Movement Controls
        
        _cameraInput = _Controls.CameraMovement.ReadValue<Vector2>();
        
        #endregion

        #region Turret Controls

        if (_Controls.TurretFire.ReadValue<float>() > 0 & _isWeaponised) TryShoot();
        
        #endregion

        #endregion
    }

    private void LateUpdate()
    {
        #region Camera Movement
        
        if (_isWeaponised)
        {
            _cameraInput = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")) * (_Sensitivity);
            CameraMovement();
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if (!_isWeaponised) CameraMovement();
    }


    private void CameraMovement()
    {
        // Moved the camera rotation to the LateUpdate method to prevent jittering
        float currentY = gameObject.transform.eulerAngles.y;
        float newRotationY = Mathf.Clamp((currentY > 180 ? currentY - 360 : currentY) + _cameraInput.x * _cameraMoveSpeed, -60f, 60f);

        float currentX = gameObject.transform.eulerAngles.x;
        float newRotationX = Mathf.Clamp((currentX > 180 ? currentX - 360 : currentX) + _cameraInput.y * _cameraMoveSpeed, -60f, 60f);
        

        gameObject.transform.eulerAngles = new Vector3(newRotationX, newRotationY, gameObject.transform.eulerAngles.z);
    }
    

    protected override void Shoot()
    {
        Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out var hit, gunData.range, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * gunData.range, Color.red);
        if (hit.collider is not null)
        {
            hit.transform.gameObject.SetActive(false);
            
            Debug.Log(gunData.gunName + " Shot " + hit.transform.gameObject.name +  " for " + gunData.damage + " damage");
        }
        else
        {
            Debug.Log(gunData.gunName + " Missed");
        }
    }
}
