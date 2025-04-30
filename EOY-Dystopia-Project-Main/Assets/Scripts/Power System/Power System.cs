using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    private int _lastThresholdTriggered = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = GameManager.Instance.bunkerData.BunkerHealth / GameManager.Instance.bunkerData.BunkerHealth;
        int threshold = Mathf.FloorToInt(healthPercent * 100 / 25) * 25;

        
        if (threshold < _lastThresholdTriggered)
        {
            _lastThresholdTriggered = threshold;
            Shutdown(threshold);
        }
    }
    
    public void PowerOn()
    {
        // Logic to power on the system
        Debug.Log("Powering on the system...");
    }
    
    public void PowerOff()
    {
        // Logic to power off the system
        Debug.Log("Powering off the system...");
    }

    private void Shutdown(int threshold)
    {
        switch (threshold)
        {
            case 75:
                ShutdownCalculation(1,6);
                break;
            case 50:
                ShutdownCalculation(1,5);
                break;
            case 25:
                ShutdownCalculation(1,4);
                break;
            case 0:
                ShutdownCalculation(1,3);
                break;
        }

        void ShutdownCalculation(int lowerbound, int upperbound)
        {
            var range = Random.Range(lowerbound, upperbound);
            if (range == 1)
            {
                PowerOff();
            }
        }
    }
}
