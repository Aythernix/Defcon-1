using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("SpawnerConfig")]
    [SerializeField]
    private GameObject[] _spawners;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject _enemyParent;
    
    [Header("EnemySave")]
    [SerializeField]
    private EnemySave _enemySave;
    [SerializeField]
    private List<GameObject> _enemies;

    [Header("EnemySceneConfig")] 
    [SerializeField]
    private float MaxEnemies = 20;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _enemies = new List<GameObject>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        var enemiesToRemove = new List<GameObject>();

        foreach (var enemy in _enemies)
        {
            if (enemy == null)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (var enemy in enemiesToRemove)
        {
            _enemies.Remove(enemy);
        }
        
        if (_enemies.Count > MaxEnemies)
        {
            // Remove excess enemies
            for (int i = _enemies.Count - 1; i >= MaxEnemies; i--)
            {
                Destroy(_enemies[i]);
                _enemies.RemoveAt(i);
            }
        }
    }

    private void OnDisable()
    {
        Debug.Log("Save Enemies Started");
        SaveEnemies();
        if (_enemySave != null)
        {
            _enemySave.enemyTransforms.Clear();
            _enemySave.enemyHealths.Clear();
        }
        else
        {
            Debug.LogWarning("_enemySave is null. Cannot clear enemy data.");
        }
    }
    private void OnEnable()
    {
        
        
    }

    private IEnumerator SpawnEnemies()
    {
        
        // Check if the number of enemies is less than the maximum allowed
        if (_enemies.Count < MaxEnemies)
        {
            for (int i = 0; i < 3; i++)
            {
                // Spawn enemies at each spawner
                foreach (GameObject spawner in _spawners)
                {
                    _enemies.Add(Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, _enemyParent.transform));
                }
            }

            yield return new WaitForSeconds(15f);

            StartCoroutine(SpawnEnemies());
        }
    }
    
    public void SaveEnemies()
    {
        
      
        
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
            {
                _enemySave.enemyTransforms.Add(enemy.transform.position);
                _enemySave.enemyHealths.Add(enemy.GetComponent<Enemy>().currentHealth);
            }
        }

        Debug.Log("Saved Completed");
    }
    public void LoadEnemies()
    {
        if (_enemySave.enemyTransforms.Count > 0)
        {
            for (int i = 0; i < _enemySave.enemyTransforms.Count; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, _enemySave.enemyTransforms[i], Quaternion.identity, _enemyParent.transform);
                enemy.GetComponent<Enemy>().currentHealth = _enemySave.enemyHealths[i];
                _enemies.Add(enemy);
            }
           
        }
        else
        {
            Debug.Log("No enemies to load");
        }
        
    }
}
