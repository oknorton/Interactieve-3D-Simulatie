using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorTagRifle : XRSocketInteractor
{
    [Header("Filter PARAMS")]
    [SerializeField] protected bool doFilterByTag = true;
    [SerializeField] protected string[] filterTags;
 
 
    public override bool CanHover(IXRHoverInteractable interactable)
    {
        if (doFilterByTag && interactable is MonoBehaviour gameObject)
        {
            foreach (string filterTag in filterTags)
            {
                if (!gameObject.CompareTag(filterTag))
                {
                    return false;
                }
                
            }
        }
        return base.CanHover(interactable);
    }
 
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (doFilterByTag && interactable is MonoBehaviour gameObject)
        {
            foreach (string filterTag in filterTags)
            {
                if (!gameObject.CompareTag(filterTag))
                {
                    return false;
                }
            }
            
        }
        return base.CanSelect(interactable);
    }
}