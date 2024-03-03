using UnityEngine;

public class EnemyBehaviour : HealthSystem
{
    public float detectionRadius, chaseRadius, speed;
    private Transform playerTransform;

    new void Start()
    {
        base.Start(); 
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer > chaseRadius)
        {
            StopMoving();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.position += direction * speed * Time.deltaTime;
    }

    void StopMoving()
    {
    }
}