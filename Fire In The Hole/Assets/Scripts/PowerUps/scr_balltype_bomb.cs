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
    public Renderer bombRenderer;
    public Color blinkColor = Color.red;
    public float blinkInterval = 0.5f;

    //Blink material stuff
    public Material normalMaterial;
    public Material blinkMaterial;
    private bool isBlinking = false;

    private bool isTriggered = false;
    private Rigidbody2D rb;
    private Color originalColor;
    public GameObject explosion;
    public scr_golfBall golfBall;
    public bool active = false;

    public bool paused = false;
    public float timer = 0;
    void Start()
    {
        bombRenderer.enabled = true;
        rb = GetComponent<Rigidbody2D>();
        if (bombRenderer && bombRenderer.material.HasProperty("_Color"))
        {
            originalColor = bombRenderer.material.color;
        }
    }

    void Blink()
    {
        if (spriteRenderer)
        {
            spriteRenderer.color = spriteRenderer.color == originalColor ? blinkColor : originalColor;
        }
        if (bombRenderer && bombRenderer.material.HasProperty("_Color"))
        {
            Color currentColor = bombRenderer.material.color;
            bombRenderer.material = isBlinking ? normalMaterial : blinkMaterial;
            isBlinking = !isBlinking;
        }


    }

    void Update()
    {
        if (golfBall.playerHitter != null && golfBall.balltype == 1) //checks if ball was hit by a player and if the power up is active (needs changing for ball type)
        {
            isTriggered = true;
            InvokeRepeating("Blink", 0f, blinkInterval);
            //paused is true when eaten by a dinosaur, to avoid a bug that will disable the dino if the bomb is eaten and explodes while eaten.
            if (!paused) timer += Time.deltaTime;
            if (timer > bombTimer) Explode();
        }
    }

    public void Explode()
    {
        CancelInvoke("Blink");
        if (spriteRenderer)
        {
            spriteRenderer.color = originalColor;
        }
        if (bombRenderer)
        {
            bombRenderer.material = normalMaterial;
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
