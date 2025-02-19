using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class scr_balltype_bomb : MonoBehaviour
{
    public float bombTimer;
    public float bombRadius;
    public float explosionForce = 500f; 
    public GameObject explosionEffect;
    public Animator explosionAnimator; 
    public SpriteRenderer spriteRenderer; 
    public Color blinkColor = Color.red;
    public float blinkInterval = 0.5f;

    private bool isTriggered = false;
    private Rigidbody2D rb;
    private Color originalColor;
    public GameObject explosion;
    public scr_golfBall golfBall;
    public bool active = false;

    void Start()
    {
        spriteRenderer.enabled = true;
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void Blink()
    {
        if (spriteRenderer)
        {
            spriteRenderer.color = spriteRenderer.color == originalColor ? blinkColor : originalColor;
        }
    }

    void Update()
    {
        if (golfBall.playerHitter != null && golfBall.balltype == 1) //checks if ball was hit by a player and if the power up is active (needs changing for ball type)
        {
            isTriggered = true;
            InvokeRepeating("Blink", 0f, blinkInterval);
            Invoke("Explode", bombTimer);
        }
    }

    public void Explode()
    {
        CancelInvoke("Blink");
        if (spriteRenderer)
        {
            spriteRenderer.color = originalColor;
        }

        if (explosionAnimator) //trigger explode animation
        {
            explosionAnimator.SetTrigger("Explode");
        }

        if (explosion) //instantiate explosion
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, bombRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody2D nearbyRb = nearbyObject.GetComponent<Rigidbody2D>();
            if (nearbyRb)
            {
                Vector2 explosionDirection = (nearbyRb.transform.position - transform.position).normalized;
                nearbyRb.AddForce(explosionDirection * explosionForce);
            }
        }

        active = false;
        //Destroy the golf ball
        Destroy(this.gameObject);
    }
}
