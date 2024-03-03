using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform shotPoint;
    public GameObject bulletPrefab;

    public float bulletForce = 30f;
    
    public void OnMouse1(InputAction.CallbackContext context)
    {
        // context.
        Shoot();
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
