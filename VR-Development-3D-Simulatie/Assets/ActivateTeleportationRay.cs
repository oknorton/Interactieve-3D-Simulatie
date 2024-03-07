using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{

    public GameObject leftTeleportation;

    public InputActionProperty leftActivate;

    public InputActionProperty leftCancel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 leftActivateValue = leftActivate.action.ReadValue<Vector2>();
        leftTeleportation.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActivateValue != Vector2.zero);
    }

}
