using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerSystem : MonoBehaviour
{

    public bool isPowerActive = true;
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnTurretCooldown += Shutdown;
    }
    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnTurretCooldown -= Shutdown;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PowerOn()
    {
        isPowerActive = true;
        // Logic to power on the system
        Debug.Log("Powering on the system...");
    }
    
    public void PowerOff()
    {
        isPowerActive = false;
        
        // Logic to power off the system
        Debug.Log("Powering off the system...");
    }

    private void Shutdown(bool state)
    {
        if (state)
        {
            var range = Random.Range(1, 11);
            Debug.Log(range);
            if (range == 1)
            {
                PowerOff();
            }
        }
    }
}
