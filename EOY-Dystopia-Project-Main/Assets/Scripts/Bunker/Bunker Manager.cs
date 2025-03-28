using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerManager : MonoBehaviour
{
    [Header("Bunker Config")]
    [SerializeField]
    private BunkerData _bunkerData;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _bunkerData.BunkerHealth = _bunkerData.BunkerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _bunkerData.BunkerHealth = Mathf.Clamp(_bunkerData.BunkerHealth, 0, _bunkerData.BunkerMaxHealth);
    }
    
    public void TakeDamage(float damage)
    {
        _bunkerData.BunkerHealth -= damage;
        
        if (_bunkerData.BunkerHealth <= 0)
        {
            DestroyBunker();
        }
    }
    
    public void RepairBunker(float repairAmount)
    {
        _bunkerData.BunkerHealth += repairAmount;
        
    }

    private void BunkerDamage()
    {
        // Change bunker model to damaged version
    }
    
    private void DestroyBunker()
    {
        Debug.Log("Health:" + _bunkerData.BunkerHealth + "Game Over");
        // End Game
        
    }
    
    
  
}
