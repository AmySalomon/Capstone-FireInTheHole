using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfAimLaser : MonoBehaviour
{
    [HideInInspector] private LineRenderer lineRenderer;
    [HideInInspector] public SpriteRenderer mySprite;
    [HideInInspector] public Outline myOutline;
    private Transform myTransform;
    [HideInInspector] private float bounceDistance = 100;

    [HideInInspector] public Color myLaserColor = Color.green;
    [HideInInspector] public Gradient myGradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private bool tryingToLaser = false;

    float lineWidth;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mySprite = GetComponent<SpriteRenderer>();
        myOutline = GetComponent<Outline>();
        myTransform = GetComponent<Transform>();
        lineWidth = lineRenderer.startWidth;
        SetGradient(myLaserColor);
    }

    private void Update()
    {
        lineRenderer.colorGradient = myGradient;
        lineRenderer.material.mainTextureScale = new Vector2(1f / lineWidth, 1.0f);
    }
    private void LateUpdate()
    {
        if (!tryingToLaser)
        {
            myOutline.enabled = true;
            lineRenderer.enabled = false;
        }
        else
        {
            tryingToLaser = false;
        }
    }

    //this method is being activated from scr_meleeSwing
    public void TryGolfLaser(Vector3 aimDirection, float swingForce, Color newColor)
    {
        tryingToLaser = true;
        myOutline.enabled = false;
        lineRenderer.enabled = true;

        SetGradient(newColor);
        Vector3 firstEndPoint;
        Vector3 bounceEndPoint;
        RaycastHit2D hitObject = Physics2D.CircleCast(myTransform.position, 0.5f * 0.4f, aimDirection, swingForce/200, 1 << LayerMask.NameToLayer("Wall"));
        if (hitObject.collider == null)
        {
            firstEndPoint = myTransform.position + aimDirection * swingForce / 200;
            bounceEndPoint = firstEndPoint;
        }
        else
        {
            firstEndPoint = hitObject.point;
            //reflect is multiplied by .9 to account for wackiness that comes from bounciness in materials
            bounceEndPoint = hitObject.point + Vector2.Reflect(aimDirection, hitObject.normal * 0.9f);
        }

        //i cannot for the life of me to get bounces to be accurate, so I am scrapping this and just going to make the bounce be very small
        //RaycastHit2D bounceHitObject = Physics2D.CircleCast(hitObject.point + hitObject.normal, 0.5f * 0.4f, Vector3.Reflect(aimDirection, hitObject.normal * 0.9f), bounceDistance, 1 << LayerMask.NameToLayer("Wall"));

        //again, reflect is multiplied by .9 to account for wackiness that comes from bounciness in materials
        DrawLaser(myTransform.position, firstEndPoint, bounceEndPoint);
    }

    void DrawLaser(Vector3 startPosition, Vector3 endPosition, Vector3 bounceEndPosition)
    {
        lineRenderer.SetPosition(0, startPosition + new Vector3(0, 0, -0.001f));
        lineRenderer.SetPosition(1, endPosition + new Vector3(0, 0, -0.001f));
        lineRenderer.SetPosition(2, bounceEndPosition + new Vector3(0, 0, -0.001f));
        //Debug.Log("RAYCAST HIT POSITION: " + endPosition);

    }

    void SetGradient(Color newGradientColor)
    {
        //for some god awful reason, unity is deleting my gradients unless I do this nonsense.
        myGradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0].color = newGradientColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = newGradientColor;
        colorKey[1].time = 1.0f;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;
        myGradient.SetKeys(colorKey, alphaKey);
    }


}
