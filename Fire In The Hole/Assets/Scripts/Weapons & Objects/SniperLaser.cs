using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperLaser : MonoBehaviour
{
    [HideInInspector] private LineRenderer lineRenderer;
    private Transform myTransform;
    [SerializeField] private float rayDistance = 300;

    [HideInInspector] public Color myColor;

    public scr_meleeSwing golfSwing;
    public ShootProjectile gunType;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        myTransform = GetComponent<Transform>();
        lineRenderer.SetColors(myColor, myColor);
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TryLaser();
    }

    void TryLaser()
    {

         RaycastHit2D hitObject = Physics2D.Raycast(myTransform.position, -transform.up, 1000, 1 << LayerMask.NameToLayer("Wall"));
        if (!golfSwing.isCharging && gunType.hasLaser)
        {
            lineRenderer.enabled = true;
            DrawLaser(myTransform.position, hitObject.point);
        }
        else
            lineRenderer.enabled = false;

    }

    void DrawLaser(Vector3 startPosition, Vector3 endPosition)
    {
        lineRenderer.SetPosition(0, startPosition + new Vector3(0, 0, -0.001f));
        lineRenderer.SetPosition(1, endPosition + new Vector3(0, 0, -0.001f));
        //Debug.Log("RAYCAST HIT POSITION: " + endPosition);
    }
}
