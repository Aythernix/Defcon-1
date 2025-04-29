using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shutdown();
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

    private void Shutdown()
    {
        if(GameManager.Instance.bunkerData.BunkerHealth % 25 == 0 && GameManager.Instance.bunkerData.BunkerHealth < GameManager.Instance.bunkerData.BunkerMaxHealth && GameManager.Instance.bunkerData.BunkerHealth > 0)
        {
            Debug.Log("Shutdown triggered");
            var range = Random.Range(1, 6);
            if (range == 1)
            {
                PowerOff();
            }
        }
    }
}
