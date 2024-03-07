using UnityEngine;

public class EnemyBehaviour : HealthSystem
{
    public float detectionRadius, chaseRadius, speed;
    public Transform shotPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 4f;
    private float nextFireTime = 0f;
    private Transform playerTransform;
    private bool isShooting = false;

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
            if (!isShooting && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f; 
                Invoke("Shoot", 1f); 
                isShooting = true;
            }
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
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotPoint.up * bulletForce, ForceMode.Impulse);
        isShooting = false; 
    }
}