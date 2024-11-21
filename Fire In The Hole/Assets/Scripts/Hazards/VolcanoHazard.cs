using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoHazard : MonoBehaviour
{
    Color startingColour;
    public float timeToImpactMax = 6;
    public float timeToImpact;
    public double lingerTimer = 1;
    private bool exploding = false;
    public Animator lavaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Collider2D>().enabled = false;
        timeToImpact = timeToImpactMax;
        this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        startingColour = GetComponent<SpriteRenderer>().color;
        exploding = false;
        ColorChanger();

    }

    // Update is called once per frame
    void Update()
    {
        timeToImpact -= Time.deltaTime;
        if (exploding)
        {//go away after set time
            lingerTimer -= Time.deltaTime;
            if(lingerTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }//when timer elapses, make area harmful
        else if(timeToImpact <= 0)
        {
            this.GetComponent<Collider2D>().enabled = true;
            exploding = true;
            lavaAnimator.SetBool("Fade Away", true);
        }
    }

    //area slowly becomes redder to indicate imminent impact
    void ColorChanger()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(startingColour, Color.red, timeToImpactMax);
    }
}
