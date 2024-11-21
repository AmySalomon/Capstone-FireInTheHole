using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointAtVector : MonoBehaviour
{
    public scr_mouseAim mouseScr;

    [HideInInspector] public Vector2 aim;
    private Vector2 lastAimDir;
    private float angle;
    public bool InputDevice; //True == Controller, false == Keyboard & Mouse

    //Mouse Variables hehe
    private Vector3 aimDirection;
    private Camera mainCamera;
    private Vector3 screenCenter;

    [SerializeField] private float aimRadius = 5f;

    private void Awake()
    {
        mainCamera = Camera.main;
        //Cursor.visible = false; //for the radius aiming 
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
    }

    public void IsAiming(Vector2 vector)
    {
        if (InputDevice == true)
        {
            aim = vector;
            if (aim.magnitude < 0.125) //stick drift deadzone
            {
                aim = Vector2.zero;
            }
        }
        if (InputDevice == false)
        {
            Debug.Log("Aiming with Mouse");
        }
    }
    void Update()
    {
        if (InputDevice == true)
        {
            if (aim == new Vector2(0, 0))
            {
                aim = lastAimDir;
            }
            else
            {
                lastAimDir = aim;
            }
            transform.transform.right = -aim;
        }
        if (InputDevice == false) 
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 offset = mouseScreenPosition - screenCenter;

            if (offset.magnitude > aimRadius)
            {
                offset = offset.normalized * aimRadius;
            }

            Vector3 clampedMouseScreenPosition = screenCenter + offset;
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(clampedMouseScreenPosition);
            aimDirection = (mouseWorldPosition - mainCamera.ScreenToWorldPoint(screenCenter)).normalized;
            transform.right = -aimDirection;
        }
    }
}
