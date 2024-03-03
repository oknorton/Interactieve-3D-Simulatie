using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; 
    public float destroyDelay = 3f;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    void OnTriggerEnter(Collider other)
    {
        StopCoroutine(DestroyAfterDelay());
        Destroy(gameObject);

        HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}