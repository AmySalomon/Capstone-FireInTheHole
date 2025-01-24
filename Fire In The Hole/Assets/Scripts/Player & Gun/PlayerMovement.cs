using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private float sandMoveSpeed = 1f;

    [SerializeField] private float rotationSpeed = 1f;

    public float currentMoveSpeed;

    private string sandTrapTag = "Sand";

    [HideInInspector] public Vector2 movement;

    private Rigidbody2D rb;

    private Dash dashCheck;

    [SerializeField] private GameObject sprite;

    private Vector3 currentRotation;

    private bool facingLeft = false;
    private bool facingRight = false;

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


        //checks which direction the player should be facing
        if (rb.velocity.x > 0)
        {
            facingLeft = false;
            facingRight = true;
        }

        if (rb.velocity.x < 0)
        {
            facingLeft = true;
            facingRight = false;
        }

        //checks what the rotation of the object is right now
        currentRotation = sprite.transform.localEulerAngles;

        //starts code to rotate towards the proper direction
        if (facingLeft)
        {
            RotateToLeft();
        }

        if (facingRight)
        {
            RotateToRight();
        }
    }

    void RotateToLeft()
    {
        //if already rotated to the specified rotation, stop rotating and stay there
        if (currentRotation.y >= 180 )
        {
            sprite.transform.localEulerAngles = new Vector3(currentRotation.x, -180, currentRotation.z);
        }

        else
        {
            sprite.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y + rotationSpeed * Time.deltaTime, currentRotation.z);
        }
    }

    void RotateToRight()
    {
        //if already rotated to the specified rotation, stop rotating and stay there
        if (currentRotation.y <= 0 || currentRotation.y > 190)
        {
            sprite.transform.localEulerAngles = new Vector3(currentRotation.x, 0, currentRotation.z);
        }

        else
        {
            sprite.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y - rotationSpeed * Time.deltaTime, currentRotation.z);
        }
    }



    //code for sand traps
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
