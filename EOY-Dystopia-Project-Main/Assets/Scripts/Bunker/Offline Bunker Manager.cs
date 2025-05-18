using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineBunkerManager : MonoBehaviour
{
    private float _enemyCount;
    [SerializeField] private float _damagePerTick;
    [SerializeField]private BunkerData _bunkerData;
    
    public float damageMultiplier = 1.2f;
    public float damageTickRate = 4f;
    
    private Coroutine damageCoroutine;
    
    
    
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

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ApplyDamage());
            }
        }
        else if (scene.name == "Outside Bunker")
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
    
    private IEnumerator ApplyDamage()
    {
        Debug.Log($"Starting damage calculation with enemy count: {_enemyCount}");
        if (SceneManager.GetActiveScene().name == "Inside Bunker" && _enemyCount > 0)
        {
            yield return new WaitForSeconds(damageTickRate);
        
            _bunkerData.BunkerHealth -= _damagePerTick;
            _bunkerData.BunkerHealth = Mathf.Clamp(_bunkerData.BunkerHealth, 0, _bunkerData.BunkerMaxHealth);

            _damagePerTick += 2f;
            
            Debug.Log($"Damage applied: {_damagePerTick} to Bunker Health: {_bunkerData.BunkerHealth}");
            StartCoroutine(ApplyDamage());
        }
        

    }
   
}

