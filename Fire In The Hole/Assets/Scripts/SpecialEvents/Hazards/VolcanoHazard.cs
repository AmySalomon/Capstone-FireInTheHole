using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoHazard : MonoBehaviour
{
    Color startingColour;
    public float timeToImpactMax;
    public float timeToImpact;
    public double lingerTimer = 0.3;
    private bool exploding = false;
    public Animator lavaAnimator;

    //added as part of ring indicator implementation
    public float timeToAppear = .3f;
    float endScale;
    float startScale;
    float newRingScale;
    //vert scale is specifically for the animation of the circles appearing at the start
    float newVertScale;

    public GameObject innerRing;
    public GameObject outerRing;
    public GameObject warningIcon;

    //timer specifically for the ring indicators, needs to count upwards.
    float time = 0;

    //soundPlayer
    private AudioSource audioPlayer;

    //Explosion audio
    public AudioClip meteorExplosion;

    //One Explosion Bool
    bool explosionSound = true;

    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //making endScale (the end of the lerp) the same size as the smaller ring. making startScale (the start of the lerp) the same size as the bigger ring.
        //using X makes it possible to make it a float, and x/y should be the same, anyway.
        endScale = innerRing.transform.localScale.x;
        startScale = outerRing.transform.localScale.x;

        timeToImpact = timeToImpactMax;
    }

    // Update is called once per frame
    void Update()
    {
        //animations for the ring indicators appearing and then closing as it approaches
        AppearAnimation();
        SpawnAnimation();
        timeToImpact -= Time.deltaTime;
        time += Time.deltaTime;

       
        if(timeToImpact <= 0)
        {
            //Audio Player <3
            audioPlayer = GetComponent<AudioSource>();
            //Play Meteor Sound
            audioPlayer.pitch = Random.Range(0.9f, 1.1f);
            audioPlayer.PlayOneShot(meteorExplosion, 1f);
            var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            explosion.transform.localScale = new Vector3 (1.3f, 1.3f, 1);
            Destroy(this.gameObject);
        }
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
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void SpawnAnimation()
    {
        //if it hasnt been as long as the spawn timer, then continue scaling down the outer ring until the timer ends
        if (time < timeToImpactMax)
        {
            newRingScale = Mathf.Lerp(startScale, endScale, time / timeToImpactMax);
            outerRing.transform.localScale = new Vector3(newRingScale, newRingScale, 1);
        }
        else
        {
            //if the lerp has ended, snap the circles to be the same size
            outerRing.transform.localScale = innerRing.transform.localScale;
        }
    }
}
