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
    public Camera mainCamera;
    private Vector3 screenCenter;
    public GameObject player;

    [SerializeField] private float aimRadius = 2f;

    private void Awake()
    {
        
        //Cursor.visible = true; //for the radius aiming 
        //Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        mainCamera = Camera.main;
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
           // Debug.Log("Aiming with Mouse");
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
            transform.right = -aim;
            if (transform.eulerAngles == new Vector3(transform.eulerAngles.x, 180, 0)) transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 180.01f);
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
            Vector3 mouseWorldPosition = mainCamera.ViewportToScreenPoint(clampedMouseScreenPosition);
            //Debug.Log(clampedMouseScreenPosition + " is the mouse position");
            aimDirection = (mouseWorldPosition - mainCamera.ViewportToScreenPoint(screenCenter)).normalized;
            transform.right = -aimDirection;
            if (transform.eulerAngles == new Vector3(transform.eulerAngles.x, 180, 0)) transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 180.01f);
            aim = aimDirection;
        }
    }
}
