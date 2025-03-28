using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    
    [Header("Enemy Movement Config")]
    public float speed;
    public float acceleration;
    
    [Header("Enemy Attack Config")]
    public float health;
    public float damage;
    public float attackRange;
    public float attackCooldown;
    
    [Header("Enemy Look Config")]
    public GameObject enemyPrefab;
}
