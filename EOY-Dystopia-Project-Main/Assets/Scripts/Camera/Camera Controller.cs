using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

public class CameraController : MonoBehaviour
{
       private Controls.CCTVCameraActions _Controls;
       
       private Vector2 _input;
    
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
       
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Controls
        
        _input = _Controls.CameraMovement.ReadValue<Vector2>();

        #endregion



        #region Camera Movement
        
        if (_input.x != 0 || _input.y != 0)
        {
            CameraMovement();
            Debug.Log(_input);
        }

        #endregion
       
        if (UnityEngine.Input.GetMouseButton(0))
        {
            Firing();
        }
    }

    private void CameraMovement()
    {
        float currentY = gameObject.transform.eulerAngles.y;
        float newRotationY = Mathf.Clamp((currentY > 180 ? currentY - 360 : currentY) + _input.x * _cameraMoveSpeed, -60f, 60f);

        float currentX = gameObject.transform.eulerAngles.x;
        float newRotationX = Mathf.Clamp((currentX > 180 ? currentX - 360 : currentX) + _input.y * _cameraMoveSpeed, -60f, 60f);

        gameObject.transform.eulerAngles = new Vector3(newRotationX, newRotationY, gameObject.transform.eulerAngles.z);
    }
    
    private void Firing()
    {
        Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out RaycastHit hit, _Range, LayerMask.GetMask("Enemy"));
        hit.transform.gameObject.SetActive(false);
    }
}
