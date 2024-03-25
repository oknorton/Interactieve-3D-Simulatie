using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class RifleScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireRate = 15f;
    public float fireSpeed = 80f;

    public XRBaseInteractor socketInteractor;
    public XRGrabInteractable grabbable;
    public PotionScript potionScript;

    private bool firing = false;

    void Start()
    {
        socketInteractor.selectEntered.AddListener(PotionAttached);
        socketInteractor.selectExited.AddListener(PotionDetached);

        grabbable.activated.AddListener(StartFiring);
        grabbable.deactivated.AddListener(StopFiring);
    }

    public void PotionAttached(SelectEnterEventArgs selectEnterEventArgs)
    {
        if (potionScript != null && potionScript.attachedToGun)
        {
            return;
        }

        if (selectEnterEventArgs.interactable is XRGrabInteractable gameObjectInteractable)
        {
            if (gameObjectInteractable.gameObject.CompareTag("Potion"))
            {
                potionScript = gameObjectInteractable.gameObject.GetComponent<PotionScript>();
                potionScript.attachedToGun = true;
            }
        }
    }



    private void PotionDetached(SelectExitEventArgs selectExitEventArgs)
    {
        potionScript.attachedToGun = false;
        potionScript = null;
    }

    private void StartFiring(ActivateEventArgs arg)
    {
        firing = true;
        if (potionScript != null)
        {
            StartCoroutine(FireBullets());
            
        }
    }

    private void StopFiring(DeactivateEventArgs arg)
    {
        firing = false;
    }

    IEnumerator FireBullets()
    {
        while (firing)
        {
            potionScript.DrainLiquid();
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
            Rigidbody bulletRigidbody = spawnedBullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 5f);
            yield return new WaitForSeconds(1f / fireRate);
        }
    }
}
