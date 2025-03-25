using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TurretCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera[] turrets;
    
    private int _currentTurret;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var turretCam in turrets)
        {
            turretCam.enabled = false;
        }
        
        turrets[0].enabled = true;
    }
    
    void NextTurretCamera()
    {
        _currentTurret ++;
        _currentTurret = Mathf.Clamp(_currentTurret, 0, turrets.Length - 1);
        
        turrets[_currentTurret - 1].enabled = false;
        turrets[_currentTurret].enabled = true;
    }

    void PreviousTurretCamera()
    {
        _currentTurret --;
        _currentTurret = Mathf.Clamp(_currentTurret, 0, turrets.Length - 1);
        
        turrets[_currentTurret + 1].enabled = false;
        turrets[_currentTurret].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}