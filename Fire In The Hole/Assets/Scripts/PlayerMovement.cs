using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 movement;

    private Rigidbody2D rb;

    private Dash dashCheck;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCheck = GetComponent<Dash>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (dashCheck.isDashing == false)
        {
            movement.Set(MovementManager.PlayerMovement.x, MovementManager.PlayerMovement.y);

            rb.velocity = movement * moveSpeed;
        }
    }
}
