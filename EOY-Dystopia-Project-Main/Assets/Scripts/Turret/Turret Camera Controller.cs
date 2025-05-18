using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretCameraController : MonoBehaviour
{
    public GameObject[] turrets;
    
   [SerializeField] private int _currentTurret;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var turret in turrets)
        {
            turret.GetComponent<TurretController>().isActive = false;
            turret.GetComponent<TurretController>().isActiveTurret = false;
            turret.GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
        }
        
        turrets[0].GetComponent<TurretController>().isActive = true;
        turrets[0].GetComponent<TurretController>().isActiveTurret = true;
        turrets[0].GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        
       
    }
    
    private void OnEnable()
    {
        GameManager.Instance.InputManager.InputMap.CCTVCamera.NextCam.performed += NextTurretCamera;
        GameManager.Instance.InputManager.InputMap.CCTVCamera.PrevCam.performed += PreviousTurretCamera;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.InputManager.InputMap.CCTVCamera.NextCam.performed -= NextTurretCamera;
        GameManager.Instance.InputManager.InputMap.CCTVCamera.PrevCam.performed -= PreviousTurretCamera;
    }
    
    void NextTurretCamera(InputAction.CallbackContext context)
    {
        if (_currentTurret == turrets.Length - 1)
        {
            _currentTurret = 0;
            // Disable the previous turret camera and enable the next turret camera
            turrets[^1].GetComponent<TurretController>().isActive = false;
            turrets[^1].GetComponent<TurretController>().isActiveTurret = false;
            turrets[^1].GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
            
            turrets[_currentTurret].GetComponent<TurretController>().isActive = true;
            turrets[_currentTurret].GetComponent<TurretController>().isActiveTurret = true;
            turrets[_currentTurret].GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        }
        else
        {
            _currentTurret ++;
            _currentTurret = Mathf.Clamp(_currentTurret, 0, turrets.Length - 1);
            
            // Disable the previous turret camera and enable the next turret camera
            turrets[_currentTurret - 1].GetComponent<TurretController>().isActive = false;
            turrets[_currentTurret - 1].GetComponent<TurretController>().isActiveTurret = false;
            turrets[_currentTurret - 1].GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
            
            turrets[_currentTurret].GetComponent<TurretController>().isActive = true;
            turrets[_currentTurret].GetComponent<TurretController>().isActiveTurret = true;
            turrets[_currentTurret].GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        }

        
    }

    void PreviousTurretCamera(InputAction.CallbackContext context)
    {
        if (_currentTurret == 0)
        {
            _currentTurret = turrets.Length - 1;
            // Disable the previous turret camera and enable the next turret camera
            turrets[0].GetComponent<TurretController>().isActive = false;
            turrets[0].GetComponent<TurretController>().isActiveTurret = false;
            turrets[0].GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
            
            turrets[_currentTurret].GetComponent<TurretController>().isActive = true;
            turrets[_currentTurret].GetComponent<TurretController>().isActiveTurret = true;
            turrets[_currentTurret].GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        }
        else
        {
            _currentTurret --;
            _currentTurret = Mathf.Clamp(_currentTurret, 0, turrets.Length - 1);
            
            // Disable the previous turret camera and enable the next turret camera
            turrets[_currentTurret + 1].GetComponent<TurretController>().isActive = false;
            turrets[_currentTurret + 1].GetComponent<TurretController>().isActiveTurret = false;
            turrets[_currentTurret + 1].GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
            
            turrets[_currentTurret].GetComponent<TurretController>().isActive = true;
            turrets[_currentTurret].GetComponent<TurretController>().isActiveTurret = true;
            turrets[_currentTurret].GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.UIManager.TurretCountUpdater(_currentTurret + 1);
    }
    
    
}