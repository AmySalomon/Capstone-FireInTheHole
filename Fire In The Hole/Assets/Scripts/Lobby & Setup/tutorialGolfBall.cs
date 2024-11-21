using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is a copy of the golf ball script as of milestone 5. it tells the ready manager to increase in ready count if the player scores with it to complete the tutorial
public class tutorialGolfBall : MonoBehaviour
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

    private ReadyManager readyManager;
    

    private void Start()
    {
        readyManager = GameObject.FindGameObjectWithTag("LobbyReadyManager").gameObject.GetComponent<ReadyManager>();
        //put initializing code here and delete Temp
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        myTrail = GetComponent<TrailRenderer>();
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
        }

        if (myRigidbody.velocity.x > minVelocityToKill || myRigidbody.velocity.y > minVelocityToKill || myRigidbody.velocity.x < -minVelocityToKill || myRigidbody.velocity.y < -minVelocityToKill)
        {
            gameObject.tag = "Bullet";
            mySprite.color = Color.red;
            myTrail.colorGradient = killSpeedGradient;

        }
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

    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(golfHoleTag))
        {
            
            AudioSource audio = other.GetComponent<AudioSource>();
            audio.Play();
            
            if (other.GetComponent<LobbyHoleIdentity>().isTutorialHole == true) ;
            {
                //when scoring in the tutorial hole, add a player as being "ready".
                readyManager.playersReady++;
                //switch statement checks which hole was hit to destroy the tutorial wall
                switch(other.GetComponent<LobbyHoleIdentity>().flagNumber)
                {
                    case 1:
                        readyManager.DestroyWall(1);
                        break;
                    case 2:
                        readyManager.DestroyWall(2);
                        break;
                    case 3:
                        readyManager.DestroyWall(3);
                        break;
                    case 4:
                        readyManager.DestroyWall(4);
                        break;
                }
                Destroy(other.gameObject);
            }

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
