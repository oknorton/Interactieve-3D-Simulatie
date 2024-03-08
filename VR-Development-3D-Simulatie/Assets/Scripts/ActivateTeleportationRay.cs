using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject Reticle;
    public InputActionProperty leftActivate;
    public InputActionProperty leftCancel;

    void Update()
    {
        Vector2 leftActivateValue = leftActivate.action.ReadValue<Vector2>();

        if (leftActivateValue != Vector2.zero)
        {
            leftTeleportation.SetActive(true);
            // float angle = Mathf.Atan2(leftActivateValue.y, leftActivateValue.x) * Mathf.Rad2Deg;
        }
        else
        {
            leftTeleportation.SetActive(false);
        }
    }
}