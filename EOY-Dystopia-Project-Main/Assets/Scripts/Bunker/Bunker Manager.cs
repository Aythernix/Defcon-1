using System;
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
        
    }
    
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnGameBoot += SetBunkerToMaxHealth;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnGameBoot -= SetBunkerToMaxHealth;
    }
    
    private void SetBunkerToMaxHealth()
    {
        _bunkerData.BunkerHealth = _bunkerData.BunkerMaxHealth;
    }
  

    // Update is called once per frame
    void Update()
    {
        _bunkerData.BunkerHealth = Mathf.Clamp(_bunkerData.BunkerHealth, 0, _bunkerData.BunkerMaxHealth);
        
        if (_bunkerData.BunkerHealth <= 0)
        {
            GameManager.Instance.EventManager.GameOver(true, "Failed to defend the bunker");
        }
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
