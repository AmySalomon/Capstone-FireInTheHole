using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scr_mouseAim : MonoBehaviour
{
    private Vector2 aimInput;
    private Vector2 aimAngle;
    private Vector3 aimDirection;
    private Camera mainCamera;

    [SerializeField] private float aimRadius = 5f;

    private PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; 
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();    
    }

    public void isAimingMouse(Vector2 aimAngleOutput)
    {
        aimAngleOutput = aimAngle;
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = transform.position.z;
        aimDirection = (mouseWorldPosition - transform.position).normalized;
        mouseWorldPosition = transform.position + aimDirection * Mathf.Min(aimRadius, Vector3.Distance(transform.position, mouseWorldPosition));
        transform.right = aimDirection;

    }

    private void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
