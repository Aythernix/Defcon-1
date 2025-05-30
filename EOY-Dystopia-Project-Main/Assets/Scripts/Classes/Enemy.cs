using System;
using System.Collections;
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
    public bool chasingPlayer;
    public bool attackingTarget;
    public bool isDead = false;

    [Header("Movement Config")]
    public float gravity = 9.81f;
    
    
    public float currentHealth;
    
    
  
    
    // Start is called before the first frame update
    void Start()
    {
        agent.speed = enemyData.speed;
        agent.acceleration = enemyData.acceleration;
        
        FindingTarget();

        agent.avoidancePriority = Random.Range(1, 100);
        
        currentHealth = enemyData.health;
        
        ChaseTarget();
        
        
    }
    
    private void FindingTarget()
    {
        target = GameObject.FindGameObjectWithTag("Bunker").transform;
        // Raycast to find the target position
       Ray ray= new Ray(transform.position, target.position - transform.position);
       if  (Physics.Raycast(ray, out RaycastHit hit, Single.PositiveInfinity, LayerMask.GetMask("Bunker")))
       {
           // Validates the target position
           NavMesh.SamplePosition(hit.point, out NavMeshHit hit2, 10f, NavMesh.AllAreas);
           targetPosition = hit2.position;
       }
       
    }
    
    private void EnemyAvoidance()
    {
       // Checks if the enemy is blocked by another enemy
       agent.isStopped = Physics.SphereCast(transform.position, 0.4f, transform.forward, out RaycastHit hit, 0.4f, LayerMask.GetMask("Enemy"));
       
       
       // Debugging the avoidance
       Debug.DrawRay(transform.position, transform.forward * 1f, Color.red, 0.1f);
       Debug.DrawLine(transform.position, hit.point, Color.green, 0.1f);
       Debug.DrawLine(hit.point, hit.point + Vector3.up * 1f, Color.blue, 0.1f);
    }
    

    // Update is called once per frame
    void Update()
    { 
        if (chasingPlayer)
        {
            MoveToTarget();
        }
        else
        {
            agent.isStopped = true;
        }
        
        EnemyAvoidance();
        
        Gravity();
        
        if (Vector3.Distance(transform.position, targetPosition) < enemyData.attackRange)
        {
            // Check if the attack coroutine is already running
            if (!attackingTarget)
            {
                // Start the attack coroutine
                StartCoroutine(AttackTarget());
            }
            
        }
        else
        {
            attackingTarget = false;
        }
    }
    
    private void MoveToTarget()
    {
        agent.SetDestination(targetPosition);
    }

    private void Gravity()
    {
        // Shoot a raycast downwards to check for ground
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1f, LayerMask.GetMask("Ground")))
        {
            // if the raycast doesnt hit the ground, apply gravity
            if (hit.collider == null)
            {
                Vector3 gravityForce = Vector3.down * gravity * Time.deltaTime;
                agent.Move(gravityForce);
            }
        }
        
    }

    private void ChaseTarget()
    {
        chasingPlayer = true;
    }

    private IEnumerator AttackTarget()
    {
        attackingTarget = true;
        
        target.GetComponent<BunkerManager>().TakeDamage(enemyData.damage);

        yield return new WaitForSeconds(enemyData.attackCooldown);
        
        attackingTarget = false;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        GameManager.Instance.ResourceManager.AddMoney(enemyData.money);
        Destroy(gameObject);
    }
}
