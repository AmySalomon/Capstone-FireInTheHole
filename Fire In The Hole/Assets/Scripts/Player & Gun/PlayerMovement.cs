using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    [HideInInspector] public Vector2 movement;

    private Rigidbody2D rb;

    private Dash dashCheck;

    [SerializeField] private SpriteRenderer sprite;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCheck = GetComponent<Dash>();
    }

    public void MovePlayer(Vector2 vector)
    {
        movement = vector;

        if (movement.magnitude < 0.125)
        {
            movement = Vector2.zero;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (dashCheck.isDashing == false)
        {
            rb.velocity = movement * moveSpeed;
        }

        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }

        if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }
    }
}
