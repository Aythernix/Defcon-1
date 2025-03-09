using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    [Header("AI Config")]
    public NavMeshAgent agent;
    public Transform target;
    public Vector3 targetPosition;
    
    [Header("AI States")]
    public bool chasingPlayer = true;
    public bool attackingPlayer = false;
    public bool isDead = false;
    
  
    
    // Start is called before the first frame update
    void Start()
    {
        agent.speed = enemyData.speed;
        agent.acceleration = enemyData.acceleration;
        
        targetPosition = new Vector3(target.position.x, 0, target.position.z);
    }

    // Update is called once per frame
    void Update()
    { 
        agent.SetDestination(target.position);
    }
    
    public void ChasePlayer()
    {
        chasingPlayer = true;
    }
    
    public void AttackPlayer()
    {
        attackingPlayer = true;
    }
    
    public void takeDamage(float damage)
    {
        enemyData.health -= damage;
        
        if (enemyData.health <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        isDead = true;
    }
}
