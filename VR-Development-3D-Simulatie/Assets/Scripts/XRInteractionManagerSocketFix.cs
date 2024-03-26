using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class XRInteractionManagerSocketFix : XRInteractionManager
{
    private List<IXRInteractable> m_Interactables = new List<IXRInteractable>();
    private List<IXRGrabTransformer> m_Transformers = new List<IXRGrabTransformer>();
    private HashSet<IXRInteractable> m_Processed = new HashSet<IXRInteractable>();

    protected override void ProcessInteractables(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        GetRegisteredInteractables(m_Interactables);
        m_Processed.Clear();

        foreach (var interactable in m_Interactables)
        {
            if (interactable is XRGrabInteractable)
            {
                UpdateSocketParentRecursive(interactable as XRGrabInteractable, updatePhase);
            }
            else
            {
                ProcessOnce(interactable, updatePhase);
            }
        }
    }

    // If an interactable is attached to a socket,
    // and if that socket has a parent interactable,
    // update that interactable first
    private void UpdateSocketParentRecursive(XRGrabInteractable grabInteractable, XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        var socketInteractor = GetSocketInteractor(grabInteractable);
        if (socketInteractor != null)
        {
            var socketGrabInteractable = socketInteractor.transform.GetComponentInParent<XRGrabInteractable>();
            if (socketGrabInteractable != null)
            {
                UpdateSocketParentRecursive(socketGrabInteractable, updatePhase);
            }
        }

        ProcessOnce(grabInteractable, updatePhase);
    }

    // Get the socket that this interactable is attached to, if any
    private XRSocketInteractor GetSocketInteractor(XRGrabInteractable grabInteractable)
    {
        grabInteractable.GetSingleGrabTransformers(m_Transformers);

        foreach (var transformer in m_Transformers)
        {
            if (transformer is XRSocketGrabTransformer)
            {
                var socketTransformer = transformer as XRSocketGrabTransformer;
                return socketTransformer.socketInteractor as XRSocketInteractor;
            }
        }

        return null;
    }

    // Ensure that each interactable is only processed once per phase
    private void ProcessOnce(IXRInteractable interactable, XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (m_Processed.Contains(interactable))
            return;

        interactable.ProcessInteractable(updatePhase);
        m_Processed.Add(interactable);
    }
}