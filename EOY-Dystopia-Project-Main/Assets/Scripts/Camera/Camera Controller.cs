using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Controls.CCTVCameraActions _input;
    
    // Start is called before the first frame update
    void Start()
    {
       _input = InputManager.current.inputs.CCTVCamera;
    }

    // Update is called once per frame
    void Update()
    {
        

        Vector2 input = _input.CameraMovement.ReadValue<Vector2>();
        float rotationSpeed = 25f; // Adjust the speed as needed

        if (input.x != 0)
        {
            float currentY = gameObject.transform.eulerAngles.y;
            float newRotationY = Mathf.Clamp((currentY > 180 ? currentY - 360 : currentY) + input.x * rotationSpeed * Time.deltaTime, -60f, 60f);
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, newRotationY, gameObject.transform.eulerAngles.z);
        }
        Debug.Log(input);
        
    }
}
