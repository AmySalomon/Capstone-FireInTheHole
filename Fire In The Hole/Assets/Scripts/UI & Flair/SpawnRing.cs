using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by Amy, for the purposes of indicating the spawn locations of objects (golf balls, powerups, players)
public class SpawnRing : MonoBehaviour
{
    public GameObject golfBall;
    public GameObject powerUp;
    public GameObject gunPickup;

    public GameObject innerRing;
    public GameObject outerRing;
    public GameObject insideIcon;

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
    float playerStartScale = 5.8f;

    public float playerRingOpacity = 0.1f;

    float newRingScale;
    //vert scale is specifically for the animation of the circles appearing at the start
    float newVertScale;

    [HideInInspector] public Color myColor = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        //making endScale (the end of the lerp) the same size as the smaller ring. making startScale (the start of the lerp) the same size as the bigger ring.
        //using X makes it possible to make it a float, and x/y should be the same, anyway.
        endScale = innerRing.transform.localScale.x;
        startScale = outerRing.transform.localScale.x;

        //if player spawning, then change colors and size of the rings to match the player spawn animation
        if (spawnPlayer)
        {
            outerRing.transform.localScale = new Vector3(5.8f, 5.8f, 1f);
            outerRing.GetComponent<SpriteRenderer>().color = new Vector4(myColor.r, myColor.g, myColor.b, playerRingOpacity);
            innerRing.GetComponent<SpriteRenderer>().color = myColor;
            insideIcon.GetComponent<SpriteRenderer>().color = new Vector4 (myColor.r, myColor.g, myColor.b, playerRingOpacity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if spawning player in, then do different stuff
        if (spawnPlayer)
        {
            PlayerSpawnAnimation();
        }
        else
        {
            AppearAnimation();
            SpawnAnimation();
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
            newRingScale = Mathf.Lerp(playerStartScale, endScale, time / playerTimeToSpawn);
            outerRing.transform.localScale = new Vector3(newRingScale, newRingScale, 1);
        }
        else
        {
            //if the lerp has ended, snap the circles to be the same size
            outerRing.transform.localScale = innerRing.transform.localScale;
            Destroy(this.gameObject);
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
            //add spawn powerup code later, in accordance with the SpawnManager script
        }

        Destroy(this.gameObject);
    }
}
