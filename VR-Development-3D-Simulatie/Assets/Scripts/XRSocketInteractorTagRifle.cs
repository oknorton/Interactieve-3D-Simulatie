using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorTagRifle : XRSocketInteractor
{
    public string targetTag;

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        if (interactable.CompareTag(targetTag))
        {
            PotionScript potion = interactable.GetComponent<PotionScript>();
            if (potion != null && !potion.attachedToGun && !potion.isPlugged)
            {
                return base.CanSelect(interactable);
            }
            return false;
        }
        return false;
    }
  
}