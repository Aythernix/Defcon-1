using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("SpawnerConfig")]
    [SerializeField] private GameObject[] _spawners; // Array of spawners in the scene
    [SerializeField] private GameObject enemyPrefab; // Enemy prefab to spawn
    [SerializeField] private GameObject _enemyParent; // Parent object to organize enemies in the hierarchy

    [Header("EnemySave")]
    [SerializeField] private EnemySave _enemySave;
    [SerializeField] private List<GameObject> _enemies;

    [Header("EnemySceneConfig")]
    [SerializeField] private int enemiesPerSpawner = 3; // Number of zombies each spawner should spawn
    [SerializeField] private float spawnDelay = 2f; // Delay between enemy spawns per spawner

    private bool isSpawningComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemies = new List<GameObject>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        CleanUpEnemies();
        EnsureMaxEnemies();
    }

    private void CleanUpEnemies()
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
    }

    private void EnsureMaxEnemies()
    {
        // You can optionally limit the number of enemies in the scene if needed
        // Remove excess enemies
        if (_enemies.Count > 20)  // Set your maximum number of enemies in the scene if required
        {
            for (int i = _enemies.Count - 1; i >= 20; i--)
            {
                Destroy(_enemies[i]);
                _enemies.RemoveAt(i);
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        int totalEnemiesSpawned = 0;

        foreach (GameObject spawner in _spawners)
        {
            for (int i = 0; i < enemiesPerSpawner; i++)
            {
                // Spawn an enemy at the spawner's position
                GameObject enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, _enemyParent.transform);
                _enemies.Add(enemy);  // Add the enemy to the list
                totalEnemiesSpawned++;

                // Wait before spawning the next enemy
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        // After spawning is complete, mark the process as finished
        isSpawningComplete = true;
        Debug.Log($"Total of {totalEnemiesSpawned} enemies spawned across all spawners.");
    }

    public void SaveEnemies()
    {
        // Only save if there are enemies in the scene
        if (_enemies.Count == 0)
        {
            Debug.LogWarning("No enemies to save.");
            return;
        }

        Debug.Log($"Starting Save: {_enemies.Count} enemies in the scene.");

        // Cache the current enemies to save before clearing the lists
        var cachedEnemies = _enemies.Where(e => e != null).ToList();
        Debug.Log($"Cached {cachedEnemies.Count} enemies to save.");

        // Clear the saved enemy data from the ScriptableObject
        ClearEnemies();

        // Save all cached enemies
        foreach (var enemy in cachedEnemies)
        {
            if (enemy != null)
            {
                SaveEnemyData(enemy);
            }
        }

        Debug.Log("Enemies saved successfully.");
    }

    private void SaveEnemyData(GameObject enemy)
    {
        // Assuming each enemy has a component that tracks its health
        if (enemy != null)
        {
            _enemySave.enemyTransforms.Add(enemy.transform.position);
            _enemySave.enemyHealths.Add(enemy.GetComponent<Enemy>().currentHealth);
        }
    }

    private void ClearEnemies()
    {
        _enemySave.enemyTransforms.Clear();
        _enemySave.enemyHealths.Clear();
    }

    public void LoadEnemies()
    {
        if (_enemySave.enemyTransforms.Count >= 0)
        {
            for (int i = 0; i < _enemySave.enemyTransforms.Count; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, _enemySave.enemyTransforms[i], Quaternion.identity, _enemyParent.transform);
                enemy.GetComponent<Enemy>().currentHealth = _enemySave.enemyHealths[i];
                _enemies.Add(enemy);
            }

            Debug.Log($"Loaded {_enemySave.enemyTransforms.Count} enemies.");
        }
        else
        {
            Debug.Log("No enemies to load");
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnIncomingSceneChange += ctx => SaveEnemies();
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnIncomingSceneChange -= ctx => SaveEnemies(); 
    }
}


