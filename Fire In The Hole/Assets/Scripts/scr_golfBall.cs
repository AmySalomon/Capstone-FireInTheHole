using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_golfBall : MonoBehaviour
{
    public Transform player;
    public GameObject directionArrowPrefab;
    private float interactionRange;
    public string golfHoleTag = "GolfHole";
    public string sandTrapTag = "Sand";
    public int ballValue = 5;


    private GameObject directionArrowInstance;
    private bool isPlayerInRange = false;
    private scr_meleeSwing playerGolfSwing;
    public Rigidbody2D myRigidbody;
    public SpriteRenderer mySprite;
    private TrailRenderer myTrail;

    private AudioSource audioSource;
    public AudioClip bounce1;
    public AudioClip bounce2;
    public AudioClip bounce3;
    public Gradient killSpeedGradient;
    public Gradient normalGradient;

    public float minVelocityToKill = 8;
    private float minVelocityToWallImpact = 3;
    public Renderer ballRenderer;
    private Color originalBallColor;

    bool tempPlayerCheck = false;

    bool hasMoved = false;
    //player who last hit the golfball
    public GameObject playerHitter;

    public float balltype = 0; //0 = regular golfball, 1 = bomball, 2 = scattershot, 3 = walking
    private scr_balltype_bomb scr_Balltype_Bomb;
    private scr_scattershotChild scr_Balltype_Scatter;
    private WalkingBall scr_Balltype_Walking;
    private scr_BT_Bounce scr_Balltype_Bounce;

    //Special golf ball chance variables
    [Header("Special Ball Chance")]
    public float specialChance = 0.30f;

    [Header("Special Ball Type Chances (Should sum to 1)")] //1.0 = 100% and 0.5 = 50% etc etc etc
    public float type1Chance = 0.5f;
    public float type2Chance = 0.5f;
    public float type3Chance = 0f;
    public float type4Chance = 0f;

    public GameObject bombModel;
    public GameObject ballModel;
    public string tutorialLvlText;

    [HideInInspector]public Outline outline;

    public GameObject textPopup;

    public GameObject wallHitImpact;
    private Quaternion hitDirection;
    private float newZDirection;

    private WaterSplash waterAnim;
    private void Start()
    {
        //put initializing code here and delete Temp
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        myTrail = GetComponent<TrailRenderer>();
        outline = GetComponent<Outline>();
        originalBallColor = ballRenderer.material.color;
        scr_Balltype_Bomb = GetComponent<scr_balltype_bomb>();
        scr_Balltype_Scatter = GetComponent<scr_scattershotChild>();
        scr_Balltype_Walking = GetComponent<WalkingBall>();
        scr_Balltype_Bounce = GetComponent<scr_BT_Bounce>();
        AssignRandomType();

        waterAnim = GetComponentInChildren<WaterSplash>();
    }

    private void Update()
    {
        if (myRigidbody.velocity.magnitude >= 0.1) hasMoved = true;
            
        //if speed of the golf ball reaches zero, reset who would get the point
        if (myRigidbody.velocity.magnitude <= 0.1 && hasMoved)
        {
            playerHitter.GetComponentInChildren<PlayerStatTracker>().UpdatePuttsMissed();
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
            ballRenderer.material.SetColor("_BaseColor",Color.red);
            myTrail.colorGradient = killSpeedGradient;

        }
        //once slow again, make non-lethal
        else
        {
            gameObject.tag = "Ball";
            mySprite.color = Color.white;
            ballRenderer.material.SetColor("_BaseColor", originalBallColor);
            myTrail.colorGradient = normalGradient;
        }
           
        if (balltype == 1)
        {
            Color color = mySprite.color;
            color.a = 0f;
            outline.OutlineColor = color; //outline needs alpha set to 0 or just low because it shines through the bomb model lol
        }

    }

    void AssignRandomType()
    {
        float chance = Random.value; //Roll for special ball
        Scene scene = SceneManager.GetActiveScene();
        if (chance < specialChance && scene.name != tutorialLvlText)
        {
            //Here we can change the spawn ring variable because here means that the ball WILL be special.
            //Roll for special type
            float specialRoll = Random.value;

            if (specialRoll < type1Chance)
            {
                bombModel.SetActive(true);
                ballModel.SetActive(false);
                scr_Balltype_Bomb.enabled = true;
                balltype = 1;
            }
            else if (specialRoll < type1Chance + type2Chance)
            {
                scr_Balltype_Walking.enabled = true;
                balltype = 2;
            }
            else if (specialRoll < type1Chance + type2Chance + type3Chance)
            {
                scr_Balltype_Scatter.enabled = true;
                balltype = 3;
            }
            else
            {
                scr_Balltype_Bounce.enabled = true;
                scr_Balltype_Bounce.bounceEnabled = true;
                balltype = 4;
            }
        }
        else
        {
            balltype = 0; //Normal ball
        }
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
            playerHitter.GetComponentInChildren<PlayerScore>(true).IncreaseScore(ballValue);
            AudioSource audio = other.GetComponent<AudioSource>();
            audio.Play();
            var scoreText = Instantiate(textPopup, other.transform.position, transform.rotation);
            scoreText.GetComponent<TextPopup>().myColor = outline.OutlineColor;
            scoreText.GetComponent<TextPopup>().weaponPickup = "None";
            if (balltype == 1) scr_Balltype_Bomb.Explode();
            Destroy(gameObject);
        }
        //slows ball in sand trap
        if (other.CompareTag(sandTrapTag))
        {
            myRigidbody.drag = 3;
            waterAnim.amInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //speed ball back up outside of sand trap
        if (other.CompareTag(sandTrapTag))
        {
            myRigidbody.drag = .9f;
            waterAnim.amInWater = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
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

            
            var hitImpact = Instantiate(wallHitImpact, transform.position, Quaternion.identity);
            hitImpact.transform.rotation = Quaternion.FromToRotation(transform.right, collision.GetContact(0).normal.normalized);
            

        }

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
    
}
