using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by Amy, for the purposes of indicating the spawn locations of objects (golf balls, powerups, players)
public class SpawnRing : MonoBehaviour
{
    public GameObject golfBall;
    public GameObject powerUp;
    public GameObject gunPickup;

    public GameObject skydiveExplosion;

    public GameObject innerRing;
    public GameObject outerRing;
    public GameObject flashingArrows;
    public GameObject warningIcon;
    public GameObject golfballIcon;
    public GameObject ammoIcon;
    public GameObject powerupIcon;
    public GameObject playerSkydive;

    public Sprite squareRing;
    public Sprite hexagonRing;
    public Sprite ballDrop;
    public Sprite weaponDrop;
    public Sprite powerupDrop;


    [HideInInspector] public bool spawnBall = false;
    [HideInInspector] public bool spawnGun = false;
    [HideInInspector] public bool spawnPowerup = false;
    public bool spawnPlayer;

    float time = 0;
    public float timeToSpawn = 2f;
    public float playerTimeToSpawn = 3f;
    public float timeToAppear = .3f;
    float endScale;
    float startScale;

    float newRingScale;
    //vert scale is specifically for the animation of the circles appearing at the start
    float newVertScale;

    float newSkydivePosition;

    [HideInInspector] public Color myColor = Color.white;

    [HideInInspector] public Sprite mySprite;

    //ALL OF THE FOLLOWING IS FOR HELLDIVERS RESPAWN MECHANIC
    [SerializeField] private float respawnMoveSpeed = 3f;

    [HideInInspector] public Vector2 respawnMovement;

    private Rigidbody2D respawnRb;

    public GameObject smokeExplosion;

    // Start is called before removthe first frame update
    void Start()
    {
        respawnRb = GetComponent<Rigidbody2D>();
        playerSkydive.transform.localPosition = new Vector3(0, 0, -9);
        //if player spawning, then change colors and size of the rings to match the player spawn animation
        if (spawnPlayer)
        {
            outerRing.transform.localScale = new Vector3(.45f, .45f, 1f);
            innerRing.transform.localScale = new Vector3(.25f, .25f, 1f);
            outerRing.GetComponent<SpriteRenderer>().color = myColor;
            innerRing.GetComponent<SpriteRenderer>().color = myColor;
            outerRing.GetComponent<SpriteRenderer>().sprite = hexagonRing;
            innerRing.GetComponent<SpriteRenderer>().sprite = hexagonRing;
            flashingArrows.GetComponent<SpriteRenderer>().color = new Color(myColor.r, myColor.g, myColor.b, 0.9f);
            warningIcon.GetComponent<SpriteRenderer>().color = myColor;
            playerSkydive.GetComponent<SpriteRenderer>().sprite = mySprite;
            golfballIcon.SetActive(false);
            ammoIcon.SetActive(false);
            powerupIcon.SetActive(false);
        }
        else
        {
            flashingArrows.SetActive(false);
            warningIcon.SetActive(false);
        }

        if (spawnBall)
        {
            //white
            outerRing.GetComponent<SpriteRenderer>().color = Color.white;
            innerRing.GetComponent<SpriteRenderer>().color = Color.white;
            golfballIcon.GetComponent<SpriteRenderer>().color = Color.white;
            playerSkydive.transform.localScale = new Vector3 (0.45f, 0.45f, 0.45f);
            playerSkydive.GetComponent<SpriteRenderer>().sprite = ballDrop;
            ammoIcon.SetActive(false);
            powerupIcon.SetActive(false);
        }
        else if (spawnGun)
        {
            //bright orange
            outerRing.GetComponent<SpriteRenderer>().color = new Color32(231, 100, 0, 255);
            innerRing.GetComponent<SpriteRenderer>().color = new Color32(231, 100, 0, 255);
            ammoIcon.GetComponent<SpriteRenderer>().color = new Color32(231, 100, 0, 255);
            outerRing.GetComponent<SpriteRenderer>().sprite = squareRing;
            innerRing.GetComponent<SpriteRenderer>().sprite = squareRing;
            playerSkydive.GetComponent<SpriteRenderer>().sprite = weaponDrop;
            playerSkydive.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            golfballIcon.SetActive(false);
            powerupIcon.SetActive(false);
        }

        else if (spawnPowerup)
        {
            //bright purple
            outerRing.GetComponent<SpriteRenderer>().color = new Color32(155, 52, 235, 255);
            innerRing.GetComponent<SpriteRenderer>().color = new Color32(155, 52, 235, 255);
            powerupIcon.GetComponent<SpriteRenderer>().color = new Color32(155, 52, 235, 255);
            outerRing.GetComponent<SpriteRenderer>().sprite = squareRing;
            innerRing.GetComponent<SpriteRenderer>().sprite = squareRing;
            playerSkydive.GetComponent<SpriteRenderer>().sprite = powerupDrop;
            golfballIcon.SetActive(false);
            ammoIcon.SetActive(false);
        }
        //making endScale (the end of the lerp) the same size as the smaller ring. making startScale (the start of the lerp) the same size as the bigger ring.
        //using X makes it possible to make it a float, and x/y should be the same, anyway.
        endScale = innerRing.transform.localScale.x;
        startScale = outerRing.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        AppearAnimation();
        //if spawning player in, then do different stuff
        if (spawnPlayer)
        {
            MoveIndicator();
            PlayerSpawnAnimation();
            PlayerSkydiveAnimation();
        }
        else
        {
            SpawnAnimation();
            ItemSkydiveAnimation();
        }

        time += Time.deltaTime;
    }

    void AppearAnimation()
    {

        if (time < timeToAppear)
        {
            newVertScale = Mathf.Lerp(0, 1, time / timeToAppear);
            transform.localScale = new Vector3(1, newVertScale, 1);
        }
        else
        {
            //if the lerp has ended, snap the circle to have the normal scale
            transform.localScale = new Vector3 (1, 1, 1);
        }
    }
    void SpawnAnimation()
    {
        //if it hasnt been as long as the spawn timer, then continue scaling down the outer ring until the timer ends
        if (time < timeToSpawn)
        {
            newRingScale = Mathf.Lerp(startScale, endScale, time / timeToSpawn);
            outerRing.transform.localScale = new Vector3(newRingScale, newRingScale, 1);
        }
        else
        {
            //if the lerp has ended, snap the circles to be the same size
            outerRing.transform.localScale = innerRing.transform.localScale;
            SpawnInThing();
        }
    }

    void PlayerSpawnAnimation()
    {
        //if it hasnt been as long as the spawn timer, then continue scaling down the outer ring until the timer ends
        if (time < playerTimeToSpawn)
        {
            newRingScale = Mathf.Lerp(startScale, endScale, time / playerTimeToSpawn);
            outerRing.transform.localScale = new Vector3(newRingScale, newRingScale, 1);
        }
        else
        {
            //if the lerp has ended, snap the circles to be the same size
            outerRing.transform.localScale = innerRing.transform.localScale;
            Instantiate(skydiveExplosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void PlayerSkydiveAnimation()
    {
        //plays the skydive animation for whichever player is spawning in, 1 second before spawning
        if (time > playerTimeToSpawn - 1)
        {
            newSkydivePosition = Mathf.Lerp(-9, 0, time - playerTimeToSpawn + 1 / 1);
            playerSkydive.transform.localPosition = new Vector3(0, 0, newSkydivePosition);
        }
    }

    void ItemSkydiveAnimation()
    {
        if (time > timeToSpawn - 1)
        {
            newSkydivePosition = Mathf.Lerp(-9, 0, time - timeToSpawn + 1 / 1);
            playerSkydive.transform.localPosition = new Vector3(0, 0, newSkydivePosition);
        }
    }

    void SpawnInThing()
    {
        if (spawnBall)
        {
            Instantiate(golfBall, transform.position, transform.rotation);
        }

        else if (spawnGun)
        {
            Instantiate(gunPickup, transform.position, transform.rotation);
        }

        else if (spawnPowerup)
        {
            Instantiate(powerUp, transform.position, transform.rotation);
            //add spawn powerup code later, in accordance with the SpawnManager script
        }
        Instantiate(smokeExplosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    void MoveIndicator()
    {
        if (respawnMovement.magnitude < 0.125)
        {
            respawnMovement = Vector2.zero;
        }
        respawnRb.velocity = respawnMovement * respawnMoveSpeed;
    }
}
