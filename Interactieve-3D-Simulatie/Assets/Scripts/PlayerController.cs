using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController  : HealthSystem
{
    public float speed;

    public float dashDistance;
    public float dashDuration;
    public float dashCooldown;
    private bool isDashing = false;
    private bool dashOnCooldown = false;
    
    private Vector2 move, mouseLook;
    private Vector3 rotationTarget;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnShiftAbility(InputAction.CallbackContext context)
    {
        if (context.started && !isDashing && !dashOnCooldown)
        {
            Debug.Log("Dash triggered");
            StartCoroutine(Dash());
        }
    }
    
 

    IEnumerator Dash()
    {
        isDashing = true;
        float startTime = Time.time;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + new Vector3(move.x, 0f, move.y).normalized * dashDistance;

        while (Time.time - startTime < dashDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / dashDuration);
            yield return null;
        }

        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        dashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Script Initiated");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseLook);
        if (Physics.Raycast(ray, out hit))
        {
            rotationTarget = hit.point;
        }

        if (!isDashing)
        {
            movePlayerWithAim();
        }
    }

    public void movePlayerWithAim()
    {
        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        Vector3 movement = new Vector3(move.x, 0f, move.y);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
