using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [Header("Spawner Config")]
    [SerializeField] private GameObject[] _spawners;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject _enemyParent;

    [Header("Enemy Save")]
    [SerializeField] private EnemySave _enemySave;
    [SerializeField] private List<GameObject> _enemies;

    [Header("Scene Config")]
    [SerializeField] private int enemiesPerSpawner = 2;
    [SerializeField] private float spawnDelay = 10f;
    [SerializeField] private float spawnerInterval = 2f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float maxEnemies = 15f;

    private bool _isSpawningComplete = false;

    private void Start()
    {
        _enemies = new List<GameObject>();
        LoadEnemies();
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
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
        if (_enemies.Count > maxEnemies)
        {
            for (int i = _enemies.Count - 1; i >= maxEnemies; i--)
            {
                Destroy(_enemies[i]);
                _enemies.RemoveAt(i);
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        Debug.Log($"Started spawning enemies");
        int totalEnemiesSpawned = 0;

        foreach (GameObject spawner in _spawners)
        {
            for (int i = 0; i < enemiesPerSpawner; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, _enemyParent.transform);
                _enemies.Add(enemy);

                totalEnemiesSpawned++;

                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitForSeconds(spawnerInterval);
        }

        yield return new WaitForSeconds(spawnDelay);

        _isSpawningComplete = true;

        StartCoroutine(SpawnEnemies());

        Debug.Log($"Total of {totalEnemiesSpawned} enemies spawned.");
    }

    public void SaveEnemies(Scene? scene = null)
    {
        if (_enemies.Count == 0)
        {
            Debug.LogWarning("No enemies to save.");
            return;
        }

        var cachedEnemies = _enemies.Where(e => e != null).ToList();
        Debug.Log($"Cached {cachedEnemies.Count} enemies to save.");

        ClearSavedEnemies();

        foreach (var enemy in cachedEnemies)
        {
            SaveEnemyData(enemy);
        }

        Debug.Log("Enemies saved successfully.");
    }

    private void SaveEnemyData(GameObject enemy)
    {
        if (enemy != null)
        {
            _enemySave.enemyTransforms.Add(enemy.transform.position);
            _enemySave.enemyHealths.Add(enemy.GetComponent<Enemy>().currentHealth);
        }
    }

    private void ClearSavedEnemies()
    {
        _enemySave.enemyTransforms.Clear();
        _enemySave.enemyHealths.Clear();
    }

    public void LoadEnemies()
    {
        if (_enemySave.enemyTransforms.Count == 0)
        {
            Debug.Log("No enemies to load.");
            return;
        }

        for (int i = 0; i < _enemySave.enemyTransforms.Count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, _enemySave.enemyTransforms[i], Quaternion.identity, _enemyParent.transform);
            enemy.GetComponent<Enemy>().currentHealth = _enemySave.enemyHealths[i];
            _enemies.Add(enemy);
        }

        Debug.Log($"Loaded {_enemySave.enemyTransforms.Count} enemies.");
    }

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnIncomingSceneChange += SaveEnemies;
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnIncomingSceneChange -= SaveEnemies;
    }
}