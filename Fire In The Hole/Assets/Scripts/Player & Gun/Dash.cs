using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 6f;
    [SerializeField] private float dashTime = 0.3f;

    //how much time the full dash meter takes to recharge, and then the actual timer tracking the time.
    public float dashRechargeAmount = 3f;
    [HideInInspector] public float dashRechargeTimer;
    public ChargeBar dashChargeBar;

    [SerializeField] private Image FirstHalf;
    [SerializeField] private Image LastHalf;

    private float timer;
    
    private float xDirection;
    private float yDirection;

    [HideInInspector] public bool isDashing = false;

    [SerializeField] private SpriteRenderer sprite;
    private Color myColor;
    private Rigidbody2D rb;

    private TrailRenderer trail;

    private PlayerMovement playerMovement;
    private CircleCollider2D playerCollision;

    public LayerMask bullet;
    public LayerMask golfBall;
    public LayerMask none;

    private Gradient originalGradient;
    private Gradient originalDashCooldownGradient;
    public Gradient burnDashGradient;
    public Gradient burnDashCooldownGradient;

    [HideInInspector] public bool BurningDash = false;

    public SpriteRenderer burningDashSprite;

    private void Awake()
    {
        //gets player movement vector action
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<CircleCollider2D>();
        myColor = sprite.color;
        dashRechargeTimer = dashRechargeAmount / 2;
        originalGradient = trail.colorGradient;
        burningDashSprite.enabled = false;
        originalDashCooldownGradient = dashChargeBar.gradient;
    }
    void Update()
    {
        DashRechargeUpdate();
        DashAction();
        dashChargeBar.SetCharge(dashRechargeTimer);

        if (BurningDash) dashChargeBar.gradient = burnDashCooldownGradient;
        else dashChargeBar.gradient = originalDashCooldownGradient;
    }

    public void PressDash()
    {
        if (isDashing == false && dashRechargeTimer >= dashRechargeAmount/2)
        {
            Debug.Log("StartDash");
            isDashing = true;
            trail.emitting = true;
            dashRechargeTimer -= dashRechargeAmount / 2;
            xDirection = playerMovement.movement.x;
            yDirection = playerMovement.movement.y;
        }
    }

    private void DashAction()
    {
        if (timer <= dashTime && isDashing)
        {
            timer += Time.deltaTime;
            playerMovement.movement.Set(xDirection, yDirection);
            Vector2 newMovement = Vector3.Normalize(playerMovement.movement);
            rb.velocity = newMovement * dashSpeed;
            sprite.color = new Color32(0, 215, 255, 255);
            playerCollision.excludeLayers = bullet + golfBall;
            //activates when burning dash powerup is active
            if (BurningDash == true)
            {
                //slows down the dash timer, so that it lasts slightly longer
                timer -= Time.deltaTime / 3;
                //speeds up the dash
                rb.velocity *= 1.2f;
                playerCollision.gameObject.tag = "Bullet";
                trail.colorGradient = burnDashGradient;
                sprite.color = new Color32(255, 0, 0, 255);
                burningDashSprite.enabled = true;
                burningDashSprite.transform.Rotate(new Vector3(0, 0, 3));
            }
        }
        else
        {
            sprite.color = myColor;
            timer = 0;
            trail.colorGradient = originalGradient;
            trail.emitting = false;
            playerCollision.excludeLayers = none;
            isDashing = false;
            playerCollision.gameObject.tag = "Player";
            burningDashSprite.enabled = false;
        }

    }

    private void DashRechargeUpdate()
    {
        FirstHalf.color = new Color32(0, 0, 0, 0);
        LastHalf.color = new Color32(0, 0, 0, 0);
        //When NOT dashing, recharge the dash constantly
        if (!isDashing) dashRechargeTimer += Time.deltaTime;

        if (dashRechargeTimer < 0)
        {
            dashRechargeTimer = 0;
            return;
        }

        if (dashRechargeTimer >= dashRechargeAmount/2)
        {
            FirstHalf.color = new Color32(0, 247, 255, 255);
            if (BurningDash == true) FirstHalf.color = new Color32(255, 0, 0, 255);
        }

        //If dash cooldown
        if (dashRechargeTimer >= dashRechargeAmount)
        {
            LastHalf.color = new Color32(0, 247, 255, 255);
            if (BurningDash == true) LastHalf.color = new Color32(255, 0, 0, 255);
            dashRechargeTimer = dashRechargeAmount;
            return;
        }

    }
}
