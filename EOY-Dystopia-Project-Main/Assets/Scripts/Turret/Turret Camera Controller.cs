using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TurretCameraController : MonoBehaviour
{
    public GameObject[] turrets;
    
    private int _currentTurret;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var turret in turrets)
        {
            turret.GetComponent<TurretController>().enabled = false;
            turret.GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
        }
        
        turrets[0].GetComponent<TurretController>().enabled = true;
        turrets[0].GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        
        GameManager.Instance.InputManager.InputMap.CCTVCamera.NextCam.performed += ctx => NextTurretCamera();
        GameManager.Instance.InputManager.InputMap.CCTVCamera.PrevCam.performed += ctx => PreviousTurretCamera();
    }
    
    void NextTurretCamera()
    {
        _currentTurret ++;
        _currentTurret = Mathf.Clamp(_currentTurret, 0, turrets.Length - 1);

        // Disable the previous turret camera and enable the next turret camera
        turrets[_currentTurret - 1].GetComponent<TurretController>().enabled = false;
        turrets[_currentTurret - 1].GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
        turrets[_currentTurret].GetComponent<TurretController>().enabled = true;
        turrets[_currentTurret].GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
    }

    void PreviousTurretCamera()
    {
        _currentTurret --;
        _currentTurret = Mathf.Clamp(_currentTurret, 0, turrets.Length - 1);
        
        // Disable the previous turret camera and enable the next turret camera
        turrets[_currentTurret + 1].GetComponent<TurretController>().enabled = false;
        turrets[_currentTurret + 1].GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
        turrets[_currentTurret].GetComponent<TurretController>().enabled = true;
        turrets[_currentTurret].GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.UIManager.TurretCountUpdater(_currentTurret + 1);
    }
    
    
}