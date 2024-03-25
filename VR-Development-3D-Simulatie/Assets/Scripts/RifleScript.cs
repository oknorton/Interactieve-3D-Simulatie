using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class RifleScript : MonoBehaviour
{
    public XRBaseInteractor socketInteractor;
    public XRGrabInteractable grabbable;
    public PotionScript potionScript;
    public Transform spawnPoint;

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
        if (potionScript !=  null && potionScript.potionType != null)
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
        while (firing && potionScript.liquid.fillAmount <= 0.6)
        {
            potionScript.DrainLiquid();
            
            // Access properties from the PotionType
            GameObject spawnedBullet = Instantiate(potionScript.potionType.bulletPrefab, 
                                                   spawnPoint.position, 
                                                   Quaternion.identity);
            Rigidbody bulletRigidbody = spawnedBullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = spawnPoint.forward * potionScript.potionType.fireSpeed;
            
            Destroy(spawnedBullet, 5f);
            yield return new WaitForSeconds(1f / potionScript.potionType.fireRate);
        }
    }
}
