using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 3f;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    void OnCollisionEnter(Collision collision)
    {
        StopCoroutine(DestroyAfterDelay());
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}