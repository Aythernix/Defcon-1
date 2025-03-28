using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private CharacterController _characterController;

    #region Camera

    [Header("Camera Config")]
    [SerializeField] private Camera _camera;
    private readonly Vector2 _cameraRotationLock = new(-85f, 85f);
    private float _verticalRotation = 0f;
    private Vector2 _lookInput;

    #endregion

    #region Movement

    [Header("Movement Config")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _strafeSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;
    
    private Vector2 _movementInput;
    private Vector3 _velocity;

    #endregion
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    

    void Update()
    {
        
        Movement();
    }

    private void LateUpdate()
    {
        CameraMovement();
    }

    private void Movement()
    {
        if (GameManager.Instance.freezePlayerMovement)
        {
            return;
        }
        
        _movementInput = GameManager.Instance.InputManager.MovementInput.normalized;
        
        float forwardMovement = _movementInput.y * _movementSpeed;
        float strafeMovement = _movementInput.x * _strafeSpeed;

        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = forwardMovement * cameraForward + strafeMovement * cameraRight;

        if (!_characterController.isGrounded) 
        {
            _velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y = -0.5f;
        }

        _characterController.Move((moveDirection + _velocity) * Time.deltaTime);
        
    }
    
    private void CameraMovement()
    {
        if (GameManager.Instance.freezePlayerLook)
        {
            return;
        }
        
        _lookInput = GameManager.Instance.InputManager.LookInput;
        
        float horizontalRotation = _lookInput.x;
        transform.Rotate(0f, horizontalRotation, 0f);

        _verticalRotation += _lookInput.y;
        _verticalRotation = Mathf.Clamp(_verticalRotation, _cameraRotationLock.x, _cameraRotationLock.y);

        _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }
}
