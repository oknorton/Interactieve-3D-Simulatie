using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunFireAR : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireRate = 15f; 
    public float fireSpeed = 80f; 

    private bool firing = false;

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(StartFiring);
        grabbable.deactivated.AddListener(StopFiring);
    }

    private void StartFiring(ActivateEventArgs arg)
    {
        firing = true;
        StartCoroutine(FireBullets());
    }

    private void StopFiring(DeactivateEventArgs arg)
    {
        firing = false;
    }

    IEnumerator FireBullets()
    {
        while (firing)
        {
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
            Rigidbody bulletRigidbody = spawnedBullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 5f);
            yield return new WaitForSeconds(1f / fireRate);
        }
    }
}