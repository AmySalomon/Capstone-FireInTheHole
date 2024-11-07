using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointAtVector : MonoBehaviour
{
    private Vector3 mouse_pos;
    private Vector3 object_pos;

    [HideInInspector] public Vector2 aim;
    private Vector2 lastAimDir;
    private float angle;

    public void IsAiming(Vector2 vector)
    {
        aim = vector;
        if (aim.magnitude < 0.125)
        {
            aim = Vector2.zero;
        }
    }
    void Update()
    {
        if (aim == new Vector2 (0,0))
        {
            aim = lastAimDir;
        }
        else
        {
            lastAimDir = aim;
        }
        transform.transform.right = -aim;

        //MOUSE CONTROLS
        /*mouse_pos = Input.mousePosition;
        mouse_pos.z = -20;
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 180);*/
    }
}
