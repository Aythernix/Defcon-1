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
    private List<GameObject> _enemies;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _enemies = new List<GameObject>();
        LoadEnemies();
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
        
        SaveEnemies();
    }

    private IEnumerator SpawnEnemies()
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
    
    public void SaveEnemies()
    {
        _enemySave.enemyTransforms.Clear();
        _enemySave.enemyHealths.Clear();
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
            {
                _enemySave.enemyTransforms.Add(enemy.transform.position);
                _enemySave.enemyHealths.Add(enemy.GetComponent<Enemy>().currentHealth);
            }
        }
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
