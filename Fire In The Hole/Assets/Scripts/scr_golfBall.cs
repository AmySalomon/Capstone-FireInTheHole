using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_golfBall : MonoBehaviour
{
    public Transform player;
    public GameObject directionArrowPrefab;
    private float interactionRange;
    public string golfHoleTag = "GolfHole";
    public string sandTrapTag = "Sand";

    private GameObject directionArrowInstance;
    private bool isPlayerInRange = false;
    private scr_meleeSwing playerGolfSwing;
    private Rigidbody2D myRigidbody;
    private SpriteRenderer mySprite;
    private TrailRenderer myTrail;

    private AudioSource audioSource;
    public AudioClip bounce1;
    public AudioClip bounce2;
    public AudioClip bounce3;
    public Gradient killSpeedGradient;
    public Gradient normalGradient;

    public float minVelocityToKill = 8;

    bool tempPlayerCheck = false;

    bool hasMoved = false;
    //player who last hit the golfball
    public GameObject playerHitter;

    [HideInInspector]public Outline outline;
    

    private void Start()
    {
        //put initializing code here and delete Temp
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        myTrail = GetComponent<TrailRenderer>();
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        if (myRigidbody.velocity.magnitude >= 0.1) hasMoved = true;
            
        //if speed of the golf ball reaches zero, reset who would get the point
        if (myRigidbody.velocity.magnitude <= 0.1 && hasMoved)
        {
          
            Debug.Log("RRRESET");
            hasMoved = false;
            playerHitter = null;
            outline.OutlineColor = Color.white;
        }

        //when moving over certain velocity, make ball lethal to players
        if (myRigidbody.velocity.x > minVelocityToKill || myRigidbody.velocity.y > minVelocityToKill || myRigidbody.velocity.x < -minVelocityToKill || myRigidbody.velocity.y < -minVelocityToKill)
        {
            gameObject.tag = "Bullet";
            mySprite.color = Color.red;
            myTrail.colorGradient = killSpeedGradient;

        }
        //once slow again, make non-lethal
        else
        {
            gameObject.tag = "Ball";
            mySprite.color = Color.white;
            myTrail.colorGradient = normalGradient;
        }
            

        /*
        if (player == null) //Temp replacement as instantialized players arent being assigned balls properly
        {
            Debug.Log("Assign Player In Inspector! [TEMP]");
        }
        else if (tempPlayerCheck == false) 
        {
            playerGolfSwing = player.GetComponent<scr_meleeSwing>();
            interactionRange = playerGolfSwing.swingDistance;

            directionArrowInstance = Instantiate(directionArrowPrefab, transform.position, Quaternion.identity);
            directionArrowInstance.GetComponent<SpriteRenderer>().enabled = false; //Hide the arrow initially
            tempPlayerCheck = true;
        }

        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        isPlayerInRange = distanceToPlayer <= interactionRange;

        if (isPlayerInRange && playerGolfSwing != null) //range check
        {
            ShowDirectionArrow();
        }
        else
        {
            HideDirectionArrow();
        }
        */

    }

    
    private void ShowDirectionArrow()
    {
        directionArrowInstance.GetComponent<SpriteRenderer>().enabled = true;

        // Calculate the direction from player to the ball
        Vector2 direction = (transform.position - player.position).normalized;

        // Rotate the arrow to point in the direction the ball will be hit
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        directionArrowInstance.transform.position = transform.position;  // Position at the ball
        directionArrowInstance.transform.rotation = Quaternion.Euler(0, 0, angle - 90);  // Rotate to point at player
    }

    private void HideDirectionArrow()
    {
       directionArrowInstance.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(golfHoleTag))
        {
            playerHitter.GetComponentInChildren<PlayerScore>(true).IncreaseScore();
            AudioSource audio = other.GetComponent<AudioSource>();
            audio.Play();
            Destroy(gameObject);
        }
        //slows ball in sand trap
        if (other.CompareTag(sandTrapTag))
        {
            myRigidbody.drag = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //speed ball back up outside of sand trap
        if (other.CompareTag(sandTrapTag))
        {
            myRigidbody.drag = .5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int golfSwing = Random.Range(1, 4);

        switch (golfSwing)
        {
            case 1:
                audioSource.PlayOneShot(bounce1);
                break;
            case 2:
                audioSource.PlayOneShot(bounce2);
                break;
            case 3:
                audioSource.PlayOneShot(bounce3);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
    
}
