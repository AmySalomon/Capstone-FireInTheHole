using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

public class scr_mouseAim : MonoBehaviour
{
    private Vector2 aimInput;
    private Vector2 aimAngle;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void isAimingMouse(Vector2 aimAngleOutput)
    {
        aimAngleOutput = aimAngle;
    }

    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(aimInput);
        aimAngle = (mouseWorldPosition - transform.position).normalized;
        transform.transform.right = -aimAngle;
        //aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    }

}
