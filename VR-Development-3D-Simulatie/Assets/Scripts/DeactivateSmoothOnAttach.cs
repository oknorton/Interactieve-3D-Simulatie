using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class DeactivateSmoothOnAttach : MonoBehaviour
{
    private void Start()
    {
        XRSocketInteractor socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnAttach);
        socket.selectExited.AddListener(OnDetach);
    }

    public void OnAttach(SelectEnterEventArgs args)
    {
        XRGrabInteractable interactable = args.interactableObject as XRGrabInteractable;
        interactable.smoothPosition = false;
        interactable.smoothRotation = false;
    }

    public void OnDetach(SelectExitEventArgs args)
    {
        XRGrabInteractable interactable = args.interactableObject as XRGrabInteractable;
        interactable.smoothPosition = true;
        interactable.smoothRotation = true;
    }
}