using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class scr_meleeSwing : MonoBehaviour
{
    [Header("Golf Swing Settings")]
    //CHANGE THESE IN THE PREFAB IN INSPECTOR!!!
    public float swingDistance = 1f;
    public float swingRadius = 1f;
    public float minSwingForce = 500f;
    public float maxSwingForce = 2000f;
    public float chargeRate = 1000f;
    public float swingCooldown = 1f;
    public int playerDamage = 20;

    private AudioSource audioPlayer;
    public AudioClip missedHit;
    public AudioClip weakHit;
    public AudioClip normalHit;
    public AudioClip strongHit;

    public Transform swingPoint;
    public Vector3 swingAim;
    public LayerMask interactableLayers;
    public LayerMask playerLayer;//for pvp player layer

    public bool isCharging = false;
    private bool canSwing = true;
    public float currentSwingForce;

    private PlayerControls controls;
    private InputAction swingAction;
    private InputAction rightStickAction;


    public Vector3 chargeBarOffset = new Vector3(0, 2, 0);
    public Vector2 chargeBarSize = new Vector2(2, 0.25f);
    private Vector2 rightStickDirection;

    public PlayerInputHandler myInput;

    public ChargeBar swingChargeBar;

    public SpriteRenderer crosshair;
    public PointAtVector gunAiming;
    public PointAtVector golfAiming;
    public SpriteRenderer golfCrosshair;

    private SpritePicker spriteObject;
    private SpriteRenderer playerSprite;

    //gets the swing animation script
    private SwingAnimation swingAnim;

    public Color outlineColor;
    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        swingChargeBar.gameObject.SetActive(false);
        spriteObject = GetComponentInChildren<SpritePicker>();
        playerSprite = spriteObject.gameObject.GetComponent<SpriteRenderer>();
        swingAnim = GetComponentInChildren<SwingAnimation>();
        golfCrosshair.enabled = false;
    }

    
    private void Update()
    {
        rightStickDirection = golfAiming.aim;

        if (rightStickDirection.magnitude > 0.1f)
        {
            swingAim = (Vector3)rightStickDirection.normalized;
        }
        else
        {
            swingAim = Vector2.zero; //unsure of what we want to do for when the player isnt aiming so figured a close range radius is fine and fun.
        }
        //Debug.Log(swingAim);
    }


    public void StartCharging()
    {
        if (canSwing)
        {
            isCharging = true;
            currentSwingForce = minSwingForce;
            StartCoroutine(ChargeSwingForce());
        }
    }

    private IEnumerator ChargeSwingForce()
    {
        while (isCharging)
        {
            currentSwingForce = Mathf.Clamp(currentSwingForce + chargeRate * Time.deltaTime, minSwingForce, maxSwingForce);
            //Debug.Log(currentSwingForce);

            gunAiming.enabled = false;
            gunAiming.gameObject.transform.Rotate(0, 0, 300 * (currentSwingForce / 200) * Time.deltaTime);
            if(playerSprite.flipX == true) gunAiming.gameObject.transform.localPosition = new Vector2(0.5f, 0);
            else gunAiming.gameObject.transform.localPosition = new Vector2(-0.5f, 0);

            golfCrosshair.enabled = true;
            crosshair.enabled = false;
            swingChargeBar.gameObject.SetActive(true);
            swingChargeBar.SetCharge(currentSwingForce);
            yield return null;
        }
    }

    public IEnumerator Swing()
    {
        isCharging = false;
        canSwing = false;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(golfCrosshair.transform.position, swingRadius, (Vector3)rightStickDirection.normalized, swingDistance, interactableLayers);

        gunAiming.gameObject.transform.localPosition = new Vector2(0, 0);
        gunAiming.enabled = true;

        golfCrosshair.enabled = false;
        crosshair.enabled = true;
        swingChargeBar.gameObject.SetActive(false);

        Debug.Log($"Swing with force: {currentSwingForce}");

        swingAnim.DoAnimation();

        foreach (RaycastHit2D hit in hits)
        {
            Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = (swingAim).normalized;
                rb.AddForce(forceDirection * currentSwingForce / 2);

                if (currentSwingForce < 600) audioPlayer.PlayOneShot(weakHit);
                else if (currentSwingForce < 1500) audioPlayer.PlayOneShot(normalHit);
                else audioPlayer.PlayOneShot(strongHit);

                //if you hit a golf ball, tell the golf ball that you hit it
                if (rb.gameObject.TryGetComponent<scr_golfBall>(out scr_golfBall golfBall))
                {
                    golfBall.playerHitter = myInput.gameObject;
                    golfBall.outline.OutlineColor = outlineColor;
                }
                if (rb.gameObject.TryGetComponent<tutorialGolfBall>(out tutorialGolfBall tutGolfBall))
                {
                    tutGolfBall.playerHitter = myInput.gameObject;
                    tutGolfBall.outline.OutlineColor = outlineColor;
                }

            }

        }
        if (hits.Length == 0)
        {
            audioPlayer.PlayOneShot(missedHit);
        }

        currentSwingForce = minSwingForce;

        yield return new WaitForSeconds(swingCooldown);
        canSwing = true;
    }

    //DEBUG STUFF
    private void OnDrawGizmosSelected()
    {
        if (swingAim != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(swingAim, swingRadius);
        }
        DrawChargeBar();
    }

    private void DrawChargeBar()
    {
        Gizmos.color = Color.green;

        float chargeRatio = (currentSwingForce - minSwingForce) / (maxSwingForce - minSwingForce);

        Vector3 barPosition = swingAim + chargeBarOffset;
        Vector3 barSize = new Vector3(chargeBarSize.x * chargeRatio, chargeBarSize.y, 1);

        Gizmos.color = Color.gray;
        Gizmos.DrawCube(barPosition, new Vector3(chargeBarSize.x, chargeBarSize.y, 1));

        Gizmos.color = Color.green;
        Gizmos.DrawCube(barPosition, barSize);
    }
}

