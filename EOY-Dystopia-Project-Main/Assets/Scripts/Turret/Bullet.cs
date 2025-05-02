using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Config")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _despawnDistance = 10f;
    
    private Vector3 _direction;
    private Vector3 _startPosition;
    // Start is called before the first frame update
    void Start()
    {
        _direction = transform.forward;
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       Move();
    }
    
    void Move()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
        
        // Check if the bullet is out of bounds
        if (Vector3.Distance(transform.position, _startPosition) > _despawnDistance)
        {
            Destroy(gameObject);
        }
    }
}
