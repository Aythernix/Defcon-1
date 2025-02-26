using UnityEngine;


public class CameraController : Gun
{
       private Controls.CCTVCameraActions _Controls;
       
       private Vector2 _cameraInput;
    
    [Header("CameraControls")]
    
    [SerializeField]
    private float _cameraMoveSpeed = .8f;
    
    [Header("GunControls")]
    
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _Range = 100f;
  
    
    // Start is called before the first frame update
    void Start()
    {
       _Controls = InputManager.current.inputs.CCTVCamera;
       
       base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Controls
        
        _cameraInput = _Controls.CameraMovement.ReadValue<Vector2>();

        #endregion


        #region Camera Movement
        
        if (_cameraInput.x != 0 || _cameraInput.y != 0)
        {
            CameraMovement();
            Debug.Log(_cameraInput);
        }

        #endregion
       
        
    }

    private void CameraMovement()
    {
        float currentY = gameObject.transform.eulerAngles.y;
        float newRotationY = Mathf.Clamp((currentY > 180 ? currentY - 360 : currentY) + _cameraInput.x * _cameraMoveSpeed, -60f, 60f);

        float currentX = gameObject.transform.eulerAngles.x;
        float newRotationX = Mathf.Clamp((currentX > 180 ? currentX - 360 : currentX) + _cameraInput.y * _cameraMoveSpeed, -60f, 60f);

        gameObject.transform.eulerAngles = new Vector3(newRotationX, newRotationY, gameObject.transform.eulerAngles.z);
    }

    public override void Update()
    {
        base.Update();
        
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }
    }

    protected override void Shoot()
    {
        Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out RaycastHit hit, gunData.range, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * _Range, Color.red);
        if (hit.collider != null)
        {
            hit.transform.gameObject.SetActive(false);
        }
    }
}
