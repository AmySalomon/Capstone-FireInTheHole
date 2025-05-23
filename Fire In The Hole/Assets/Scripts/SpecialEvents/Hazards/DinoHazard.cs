using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is for the dino hazard object. it rotates over a certain angle over time, and when a ball hits it, it will stop and shoot it back out after a short time.
public class DinoHazard : MonoBehaviour
{
    [Header("Change Z rotation for base")]
    [Header("COUNTER CLOCKWISE for max")]
    public float maxRotationDegrees = 35;
    //speed in degrees per second
    public float rotationSpeed = 5f; 
    //the two ends of the rotation axis, where it starts and where it ends
    private Quaternion firstPosition;
    private Quaternion endPosition;

    public AnimationCurve rotationCurve;

    public GameObject aimReticle;
    //whether or not the rotation is going from 1st to 2nd (true), or reverse (false)
    private bool goingToEndPos = true;
    //whether or not this hazard is currently idle or spitting the ball
    private bool tryingToSpit = false;

    public float timeToSpit;
    public float spitForce;

    private float spitTimer;

    private GameObject eatenBall;

    private MeshRenderer[] ballMeshes;
    private SpriteRenderer[] ballSprites;
    //soundPlayer
    private AudioSource audioPlayer;

    //Dino audio
    public AudioClip dinoRoar;

    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.rotation;
        endPosition = Quaternion.Euler(0, 0,firstPosition.eulerAngles.z + maxRotationDegrees);

        //Get Audio Player
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tryingToSpit)
        {
            spitTimer += Time.deltaTime;

            if (spitTimer > timeToSpit)
            {
                //effectively, turn the ball back on
                eatenBall.transform.position = aimReticle.transform.position;
                eatenBall.GetComponent<Rigidbody2D>().velocity = transform.right * spitForce;
                
                eatenBall.GetComponent<CircleCollider2D>().enabled = true;
                eatenBall.GetComponent<Outline>().enabled = true;
                eatenBall.GetComponent<TrailRenderer>().enabled = true;
                eatenBall.GetComponent<scr_balltype_bomb>().paused = false;
                ballMeshes = eatenBall.GetComponentsInChildren<MeshRenderer>();
                ballSprites = eatenBall.GetComponentsInChildren<SpriteRenderer>();
                foreach (MeshRenderer mesh in ballMeshes)
                {
                    mesh.enabled = true;
                    if (mesh.name == "inner")
                    {
                        mesh.enabled = false;
                    }
                }
                foreach (SpriteRenderer sprite in ballSprites)
                {
                    sprite.enabled = true;

                    if (sprite.name == "blinkindicator")
                    {
                        sprite.enabled = false;
                    }
                }
                //this is necessary so that the dinosaurs do not enable the legs on the walking ball sprites
                var legsSprite = eatenBall.GetComponentInChildren<FlipAnim>();
                legsSprite.GetComponent<SpriteRenderer>().enabled = false;
                //reset variables for next time
                spitTimer = 0;

                //Play Dino Sound
                audioPlayer.pitch = Random.Range(0.9f, 1.1f);
                audioPlayer.PlayOneShot(dinoRoar, 1f);

                tryingToSpit = false;
            }
        }
        //if going to end AND not trying to spit a ball, then go to end.
        else if (goingToEndPos && !tryingToSpit)
        {
            //degrees we must travel
            var angle = Quaternion.Angle(firstPosition, endPosition); 
            //angle/rotationspeed = time it'd take to travel a at such speed
            StartCoroutine(RotateToEnd(angle / rotationSpeed));
        }
        //if going back to start AND not trying to spit a ball, then go back to the end
        else if (!goingToEndPos && !tryingToSpit)
        {
            var angle = Quaternion.Angle(endPosition, firstPosition);
            StartCoroutine(RotateToFirst(angle / rotationSpeed));
        }
    }

    IEnumerator RotateToEnd(float duration)
    {
        float time = 0f;
        while(time < duration)
        {
            transform.rotation = Quaternion.Slerp(firstPosition, endPosition, rotationCurve.Evaluate(time / duration));
            
            yield return null;
            time += Time.deltaTime;
        }
        transform.rotation = endPosition;
        goingToEndPos = false;
        StopAllCoroutines();
    }


    IEnumerator RotateToFirst(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(endPosition, firstPosition, rotationCurve.Evaluate(time / duration));
            
            yield return null;
            time += Time.deltaTime;
        }
        transform.rotation = firstPosition;
        goingToEndPos = true;
        StopAllCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!tryingToSpit)
        {
            if (collision.gameObject.GetComponent<scr_golfBall>())
            {
                eatenBall = collision.gameObject;
                //effectively, turn the ball off while not deleting it
                eatenBall.GetComponent<SpriteRenderer>().enabled = false;
                eatenBall.GetComponent<CircleCollider2D>().enabled = false;
                eatenBall.GetComponent<Outline>().enabled = false;
                eatenBall.GetComponent<TrailRenderer>().enabled = false;
                eatenBall.GetComponent<scr_balltype_bomb>().paused = true;
                //this is done to make sure it doesn't slow down and reset the player who last hit it.
                eatenBall.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
                ballMeshes = eatenBall.GetComponentsInChildren<MeshRenderer>();
                ballSprites = eatenBall.GetComponentsInChildren<SpriteRenderer>();
                foreach (MeshRenderer mesh in ballMeshes)
                {
                    mesh.enabled = false;
                }
                foreach (SpriteRenderer sprite in ballSprites)
                {
                    sprite.enabled = false;
                }
                StopAllCoroutines();
                tryingToSpit = true;
            }
        }
        
    }
}
