using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class scr_meleeSwing : MonoBehaviour
{
    [Header("Golf Swing Settings")]
    public float swingDistance = 5f;
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

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        swingChargeBar.gameObject.SetActive(false);
        spriteObject = GetComponentInChildren<SpritePicker>();
        playerSprite = spriteObject.gameObject.GetComponent<SpriteRenderer>();
        golfCrosshair.enabled = false;
    }

    
    private void Update()
    {
        rightStickDirection = rightStickAction.ReadValue<Vector2>();

        if (rightStickDirection.magnitude > 0.1f)
        {
            swingAim = transform.position + (Vector3)rightStickDirection.normalized * swingDistance;
        }
        else
        {
            swingAim = transform.position; //unsure of what we want to do for when the player isnt aiming so figured a close range radius is fine and fun.
        }
    }

    private void OnSwingStarted(InputAction.CallbackContext context)

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
            gunAiming.gameObject.transform.Rotate(0, 0, 2 * (currentSwingForce / 200));
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


        RaycastHit2D[] hits = Physics2D.CircleCastAll(swingAim, swingDistance, Vector2.zero, 0f, interactableLayers);

        gunAiming.gameObject.transform.localPosition = new Vector2(0, 0);
        gunAiming.enabled = true;

        golfCrosshair.enabled = false;
        crosshair.enabled = true;
        swingChargeBar.gameObject.SetActive(false);

        Debug.Log($"Swing with force: {currentSwingForce}");


        foreach (RaycastHit2D hit in hits)
        {
            Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = (hit.collider.transform.position - swingAim).normalized;
                rb.AddForce(forceDirection * currentSwingForce);

                if (currentSwingForce < 600) audioPlayer.PlayOneShot(weakHit);
                else if (currentSwingForce < 1500) audioPlayer.PlayOneShot(normalHit);
                else audioPlayer.PlayOneShot(strongHit);

                //if you hit a golf ball, tell the golf ball that you hit it
                if (rb.gameObject.TryGetComponent<scr_golfBall>(out scr_golfBall golfBall))
                {
                    golfBall.playerHitter = myInput.gameObject;
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

    private void OnDrawGizmosSelected()
    {
        if (swingAim != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(swingAim, swingDistance);
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
