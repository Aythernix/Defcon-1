using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    [Header("AI Config")]
    public NavMeshAgent agent;
    public Transform target;
    public Vector3 targetPosition;
    
    [Header("AI States")]
    public bool chasingPlayer = true;
    public bool attackingTarget = false;
    public bool isDead = false;

    [Header("Movement Config")]
    public float gravity = 9.81f;
    
    
  
    
    // Start is called before the first frame update
    void Start()
    {
        agent.speed = enemyData.speed;
        agent.acceleration = enemyData.acceleration;
        
        FindingTarget();
        targetPosition = new Vector3(targetPosition.x, 0, targetPosition.z);

        agent.avoidancePriority = Random.Range(1, 100);
    }
    
    private void FindingTarget()
    {
        
        // Raycast to find the target position
       Ray ray= new Ray(transform.position, target.position - transform.position);
       if  (Physics.Raycast(ray, out RaycastHit hit, Single.PositiveInfinity, LayerMask.GetMask("Bunker")))
       {
           targetPosition = hit.point;
           
           
           // Validates the target position
           NavMesh.SamplePosition(targetPosition, out NavMeshHit hit2, 5, NavMesh.AllAreas);
           targetPosition = hit2.position;
       }
    }
    
    private void EnemyAvoidance()
    {
        // Draw a sphere in-front of the enemy to detect obstacles
        agent.isStopped = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1f, LayerMask.GetMask("Enemy"));
    }
    

    // Update is called once per frame
    void Update()
    { 
        MoveToTarget();
        EnemyAvoidance();
    }
    
    private void MoveToTarget()
    {
        agent.SetDestination(targetPosition);
    }

    private void Gravity()
    {
        
    }
    
    public void ChaseTarget()
    {
        chasingPlayer = true;
    }
    
    public void AttackTarget()
    {
        attackingTarget = true;
    }
    
    public void TakeDamage(float damage)
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
