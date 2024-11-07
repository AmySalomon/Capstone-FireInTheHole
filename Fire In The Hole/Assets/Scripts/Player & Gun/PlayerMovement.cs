using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private float sandMoveSpeed = 1f;

    private float currentMoveSpeed;

    private string sandTrapTag = "Sand";

    [HideInInspector] public Vector2 movement;

    private Rigidbody2D rb;

    private Dash dashCheck;

    [SerializeField] private SpriteRenderer sprite;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCheck = GetComponent<Dash>();

        //starts movespeed as current movdespeed
        currentMoveSpeed = moveSpeed;
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
            rb.velocity = movement * currentMoveSpeed;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if in sand trap, slow currentmovespeed
        if (other.CompareTag(sandTrapTag))
        {
            currentMoveSpeed = sandMoveSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if in sand trap, slow currentmovespeed
        if (other.CompareTag(sandTrapTag))
        {
            currentMoveSpeed = moveSpeed;
        }
    }
}
