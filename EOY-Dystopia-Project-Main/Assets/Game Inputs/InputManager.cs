using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager current;
    public Controls inputs;

    private void Awake()
    {
        current = this;
        
        inputs = new Controls();
        inputs.CCTVCamera.Enable();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        #region Subscriptions

        inputs.CCTVCamera.CameraMovement.performed += CameraMovementOnperformed;

        inputs.CCTVCamera.CameraMovement.canceled += CameraMovementOncanceled;

        #endregion
    }
    
    #region Performaned Methods
    
    private void CameraMovementOnperformed(InputAction.CallbackContext obj)
    {
        
    }
    
    #endregion
    
    #region Canclled Methods
    
    private void CameraMovementOncanceled(InputAction.CallbackContext obj)
    {
        
    }
    
    #endregion
    
}
