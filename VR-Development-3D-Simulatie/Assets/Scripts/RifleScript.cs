using System.Collections;
using DefaultNamespace;
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
       while (firing && potionScript.liquid.fillAmount < potionScript.empty)
       {
           potionScript.DrainLiquidOnFire();
   
           for (int i = 0; i < potionScript.potionType.numBullets; i++)
           {
               Vector3 direction = spawnPoint.forward;
               if (potionScript.potionType.bulletPattern == BulletPattern.Shotgun)
               {
                   direction = Quaternion.Euler(0, Random.Range(-potionScript.potionType.spreadAngle / 2f, potionScript.potionType.spreadAngle / 2f), 0) * direction;
               }
   
               GameObject spawnedBullet = Instantiate(potionScript.potionType.bulletPrefab, spawnPoint.position, Quaternion.identity);
               Rigidbody bulletRigidbody = spawnedBullet.GetComponent<Rigidbody>();
               bulletRigidbody.velocity = direction * potionScript.potionType.fireSpeed;
   
               Destroy(spawnedBullet, 5f);
           }
   
           yield return new WaitForSeconds(1f / potionScript.potionType.fireRate);
   
           if (potionScript.potionType.bulletPattern == BulletPattern.Burst && potionScript.potionType.numBullets > 1)
           {
               yield return new WaitForSeconds(potionScript.potionType.burstInterval);
           }
       }
   }
}
