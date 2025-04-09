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
    private GameObject _enemyParent;
    
    public GameObject enemyPrefab;


    public List<GameObject> enemies;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        var enemiesToRemove = new List<GameObject>();

        foreach (var enemy in enemies)
        {
            if (enemy == null)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (var enemy in enemiesToRemove)
        {
            enemies.Remove(enemy);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            // Spawn enemies at each spawner
            foreach (GameObject spawner in _spawners)
            {
                enemies.Add(Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, _enemyParent.transform));
            }
        }

        yield return new WaitForSeconds(15f);

        StartCoroutine(SpawnEnemies());
    }
    
    public void RespawnEnemies()
    {
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        enemies.Clear();
        
        StartCoroutine(SpawnEnemies());
    }
}
