using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    
    
    [Header("Camera Config")] 
    [SerializeField]
    private Camera _camera;
    private readonly Vector2 _cameraRotationLock = new(-90, 90);
    private Vector2 _cameraRotation;
    
    [Header("Movement Config")]
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _strafeSpeed = 5f;
    
    private CharacterController _characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _characterController = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    private void Movement()
    {
        var movementInput = GameManager.Instance.InputManager.MovementInput;
        var forwardMovement = movementInput.y * _movementSpeed;
        var strafeMovement = movementInput.x * _strafeSpeed; 
        
        var moveDirection = new Vector3(strafeMovement, 0, forwardMovement);
        moveDirection = transform.rotation * moveDirection;  
        
        _characterController.Move(moveDirection * Time.deltaTime);
    }

    private void LateUpdate()
    {
        Movement();
    }

    private void CameraMovement()
    {
        var lookInput = GameManager.Instance.InputManager.LookInput;

        var horizontalRotation = lookInput.x;
        transform.Rotate(0, horizontalRotation * Time.deltaTime, 0);
        
        _camera.transform.eulerAngles = new Vector3(math.clamp(_camera.transform.eulerAngles.x, _cameraRotationLock.x, _cameraRotationLock.y), _camera.transform.eulerAngles.y, _camera.transform.eulerAngles.z);
        
        var verticalRotation = -lookInput.y;
        _camera.transform.Rotate(verticalRotation * Time.deltaTime, 0, 0);

        Debug.Log( _camera.transform.eulerAngles);
        
    }
}

