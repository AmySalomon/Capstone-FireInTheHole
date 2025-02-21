using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mouseCrosshair : MonoBehaviour
{
    public PointAtVector PointAtVector;
    public SpriteRenderer obj_crosshair;
    public SpriteRenderer obj_Controllercrosshair;
    public Vector3 controllerPos;


    // Start is called before the first frame update
    void Start()
    {
        PointAtVector = GetComponentInParent<PointAtVector>();
        controllerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PointAtVector != null && obj_crosshair != null)
        {
            if (PointAtVector.InputDevice == false)
            {
                obj_crosshair.transform.position = PointAtVector.mouseWorldPosition;
                obj_Controllercrosshair.enabled = false;
                obj_crosshair.enabled = true;
            }
            else
            {
                // obj_crosshair.transform.position = controllerPos;
                obj_Controllercrosshair.enabled = true;
                obj_crosshair.enabled = false;
            }
        }
    }
}
