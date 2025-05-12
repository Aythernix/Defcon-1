using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineBunkerManager : MonoBehaviour
{
    private float _enemyCount;
    private float _damagePerTick;
    [SerializeField]private BunkerData _bunkerData;
    
    public float damageMultiplier = 2f;
    public float damageTickRate = 4f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.EventManager.OnSceneLoaded += DamageCalculator;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnSceneLoaded -= DamageCalculator;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Outside Bunker")
        {
           StopCoroutine(ApplyDamage());
        }
        
        if (_bunkerData.BunkerHealth <= 0)
        {
            GameManager.Instance.EventManager.GameOver(true, "Failed to defend the bunker");
        }
    }
    
    private void DamageCalculator(Scene scene)
    {
        if (scene.name == "Inside Bunker")
        {
            // Get the enemy count from the BunkerManager
            _enemyCount = GameManager.Instance.enemySave.enemyTransforms.Count;
            
            // Calculate damage per tick
            _damagePerTick = _enemyCount * damageMultiplier;

            StartCoroutine(ApplyDamage());
        }
    }
    
    private IEnumerator ApplyDamage()
    {
        if (SceneManager.GetActiveScene().name == "Inside Bunker")
        {
            Debug.Log("Starting Damage Coroutine");
            yield return new WaitForSeconds(damageTickRate);
        
            _bunkerData.BunkerHealth -= _damagePerTick++;
            _bunkerData.BunkerHealth = Mathf.Clamp(_bunkerData.BunkerHealth, 0, _bunkerData.BunkerMaxHealth);
            Debug.Log($"Bunker Health: {_bunkerData.BunkerHealth}");

            _damagePerTick++;
        
            StartCoroutine(ApplyDamage());
        }
        

    }
   
}

