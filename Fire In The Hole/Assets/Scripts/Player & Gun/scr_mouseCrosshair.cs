using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mouseCrosshair : MonoBehaviour
{
    public PointAtVector PointAtVector;
    public GameObject obj_crosshair;

    // Start is called before the first frame update
    void Start()
    {
        PointAtVector = GetComponentInParent<PointAtVector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PointAtVector != null && obj_crosshair != null)
        {
            if (PointAtVector.InputDevice == false)
            {
                obj_crosshair.transform.position = PointAtVector.mouseWorldPosition;
            }
        }
    }
}
