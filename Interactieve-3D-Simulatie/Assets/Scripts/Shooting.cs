using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform shotPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 30f;
    public float fireRate = 10f; 
    private float nextFireTime = 0f;

    private bool isShooting = false;

    public void OnMouse1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isShooting = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isShooting = false;
        }
    }

    void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; 
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotPoint.up * bulletForce, ForceMode.Impulse);
    }

    public void OnMouse2(InputAction.CallbackContext context)
    {
    }
}