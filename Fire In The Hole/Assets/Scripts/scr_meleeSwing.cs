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
    public Rigidbody2D rb;
    public Vector2 forceDirection;
    public CapsuleCollider2D meleeHitbox;

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
    public float balltype;

    public Color outlineColor;
    private void Awake()
    {
        meleeHitbox.enabled = false;
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

            //this next section will be to detect golf balls in range, and send that data to the laser aim script
            RaycastHit2D[] hits = Physics2D.CircleCastAll(golfCrosshair.transform.position, swingRadius, swingAim.normalized, swingDistance, interactableLayers);

            foreach (RaycastHit2D hit in hits)
            {
                GolfAimLaser golfBallLaser = hit.transform.gameObject.GetComponent<GolfAimLaser>();

                golfBallLaser.TryGolfLaser(swingAim, currentSwingForce, outlineColor);
                
            }
            //rumble amount based on swingforce
            myInput.rumbleTime = 0.1f;
            if (currentSwingForce < 600) myInput.rumbleAmount = 0.1f;
            else if (currentSwingForce < 1500) myInput.rumbleAmount = 0.2f;
            else myInput.rumbleAmount = 0.4f;


                yield return null;
        }
    }


    public IEnumerator Swing()
    {
        isCharging = false;
        canSwing = false;
        meleeHitbox.enabled = true;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(golfCrosshair.transform.position, swingRadius, swingAim.normalized, swingDistance, interactableLayers);

        gunAiming.gameObject.transform.localPosition = new Vector2(0, 0);
        gunAiming.enabled = true;

        golfCrosshair.enabled = false;
        crosshair.enabled = true;
        swingChargeBar.gameObject.SetActive(false);

        Debug.Log($"Swing with force: {currentSwingForce}");

        swingAnim.DoAnimation();

        foreach (RaycastHit2D hit in hits)
        {
            rb = hit.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                /*if (balltype == 1)
                {
                    rb.gameObject.GetComponent<scr_balltype_bomb>().active = true;
                    rb.gameObject.GetComponent<scr_golfBall>().balltype = 1;
                } //Remove when balltype not PU */
                forceDirection = (swingAim).normalized;
                rb.AddForce(forceDirection * currentSwingForce / 2);
                myInput.rumbleTime = 0.3f;
                if (currentSwingForce < 600)
                {
                    audioPlayer.PlayOneShot(weakHit);
                    StartCoroutine(myInput.StartRumble(0.4f, 0.1f)); //vibrate the controller based on shot strength
                }
                else if (currentSwingForce < 1500)
                {
                    audioPlayer.PlayOneShot(normalHit);
                    StartCoroutine(myInput.StartRumble(0.7f, 0.2f));
                }
                else
                {
                    audioPlayer.PlayOneShot(strongHit);
                    StartCoroutine(myInput.StartRumble(1f, 0.3f)); //vibrate the controller based on shot strength
                }

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

        //currentSwingForce = minSwingForce;

        yield return new WaitForSeconds(swingCooldown);
        meleeHitbox.enabled = false;
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

