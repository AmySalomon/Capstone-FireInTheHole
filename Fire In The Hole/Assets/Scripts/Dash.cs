using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Dash : MonoBehaviour
{
    [SerializeField] private InputActionReference dash;
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

    private PlayerInput playerInput;
    private InputAction moveAction;
    [SerializeField] private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private TrailRenderer trail;

    private Vector2 movement;
    

    private void OnEnable()
    {
        dash.action.performed += StartDash;
    }

    private void OnDisable()
    {
        dash.action.performed -= StartDash;
    }

    private void Awake()
    {
        //gets player movement vector action
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }
    void Update()
    {
        DashRechargeUpdate();
        DashAction();
        dashChargeBar.SetCharge(dashRechargeTimer);
    }

    private void StartDash(InputAction.CallbackContext obj)
    {
        if (isDashing == false && dashRechargeTimer >= dashRechargeAmount/2)
        {
            Debug.Log("StartDash");
            isDashing = true;
            trail.emitting = true;
            dashRechargeTimer -= dashRechargeAmount / 2;
            xDirection = MovementManager.PlayerMovement.x;
            yDirection = MovementManager.PlayerMovement.y;
        }
    }

    private void DashAction()
    {
        if (timer <= dashTime && isDashing)
        {
            timer += Time.deltaTime;
            movement.Set(xDirection, yDirection);
            rb.velocity = movement * dashSpeed;
            sprite.color = new Color32(0, 215, 255, 255);
            
        }
        else
        {
            sprite.color = new Color32(255, 255, 255, 255);
            timer = 0;
            trail.emitting = false;
            isDashing = false;
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
        }

        //If dash cooldown
        if (dashRechargeTimer >= dashRechargeAmount)
        {
            LastHalf.color = new Color32(0, 247, 255, 255);
            dashRechargeTimer = dashRechargeAmount;
            return;
        }

    }
}
