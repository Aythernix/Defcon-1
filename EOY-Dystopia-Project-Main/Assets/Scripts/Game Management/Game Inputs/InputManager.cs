using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; } = Vector2.zero;
    public Vector2 LookInput { get; private set; }
    public float cameraSensitivity = 30f;

    public InputMap InputMap  { get; private set; } = null;

   
    void SetMovement(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        LookInput = InputMap.Player.Look.ReadValue<Vector2>() * cameraSensitivity;
    }
    
    private void OnEnable()
    {
        InputMap = new InputMap();
        
        InputMap.Player.Enable();
        
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
