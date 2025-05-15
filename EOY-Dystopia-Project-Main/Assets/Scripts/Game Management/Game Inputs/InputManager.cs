using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; } = Vector2.zero;
    public Vector2 LookInput { get; private set; }
    public float cameraSensitivity = 0.3f;

    public InputMap InputMap  { get; private set; } = null;

   
    void SetMovement(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
    }
    
    private void LateUpdate()
    {
        LookInput = new Vector2(InputMap.Player.Look.ReadValue<Vector2>().x * cameraSensitivity, -InputMap.Player.Look.ReadValue<Vector2>().y * cameraSensitivity);
        
        // Debug.Log(InputMap.CCTVCamera.TurretAim.ReadValue<float>());
    }
    
    private void OnEnable()
    {
        InputMap = new InputMap();
        
        InputMap.Player.Enable();
        InputMap.CCTVCamera.Enable();
        InputMap.Terminal.Disable();
        
        InputMap.Player.Movement.performed += SetMovement;
        InputMap.Player.Movement.canceled += SetMovement;
        
    }
    
    private void OnDisable()
    {
        InputMap.Player.Disable();
        
        InputMap.Player.Movement.performed -= SetMovement;
        InputMap.Player.Movement.canceled -= SetMovement;
      
    }

}
