using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private InputActionReference dash;
    [SerializeField] private float dashSpeed = 6f;
    [SerializeField] private float dashTime = 0.3f;

    //how much time the full dash meter takes to recharge, and then the actual timer tracking the time.
    public float dashRechargeAmount = 3f;
    [HideInInspector] public float dashRechargeTimer;

    private float timer;
    
    private float xDirection;
    private float yDirection;

    [HideInInspector] public bool isDashing = false;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

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
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        DashRechargeUpdate();
        DashAction();
    }

    private void StartDash(InputAction.CallbackContext obj)
    {
        if (isDashing == false && dashRechargeTimer >= dashRechargeAmount/2)
        {
            Debug.Log("StartDash");
            isDashing = true;
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
            sprite.color = new Color32(255, 200, 0, 255);
            timer = 0;
            isDashing = false;
        }

    }

    private void DashRechargeUpdate()
    {
        //When NOT dashing, recharge the dash constantly
        if (!isDashing) dashRechargeTimer += Time.deltaTime;

        //If dash cooldown
        if (dashRechargeTimer >= dashRechargeAmount)
        {
            dashRechargeTimer = dashRechargeAmount;
            return;
        }

    }
}
