using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBall : MonoBehaviour
{
    public float walkDelay;
    private float timer;
    private float stepCount = 0;
    public float maxSteps;
    private bool walking;
    private Vector2 randomDirection;
    private float walkSpeed = 1;
    private bool negativeX = false;
    private bool negativeY = false;

    public SpriteRenderer walkingFeet;
    public scr_golfBall golfBall;

    // Start is called before the first frame update
    void Start()
    {
        walkingFeet.enabled = false;
        stepCount = maxSteps;
    }

    // Update is called once per frame
    void Update()
    {
        StartWalking();
        //if the golf ball isn't moving for enough time, the ball grows legs and starts to walk about.
        if (golfBall.myRigidbody.velocity.magnitude <= 0.1)
        {
            timer += Time.deltaTime;
            if (timer >= walkDelay)
            {
                golfBall.playerHitter = null;
                walking = true;
                walkingFeet.enabled = true;
                Debug.Log(walking);
            }
        }
        //if it moves, reset the timer.
        else if (!walking) timer = 0;
    }

    void StartWalking()
    {
        if(golfBall.playerHitter != null)
        {
            timer = 0;
            walkingFeet.enabled = false;
            walking = false;
        }

        if (walking)
        {
            //if stepped enough, change direction
            stepCount++;
            if (stepCount >= maxSteps)
            {
                float randomX = Random.Range(-1.0f, 1.0f);
                float randomY = Random.Range(-1.0f, 1.0f);
                if (randomX < 0) negativeX = true;
                if (randomY < 0) negativeY = true;
                randomDirection = new Vector2(randomX, randomY);
                randomDirection.Normalize();
                //if (negativeX) randomDirection.x = randomDirection.x * -1;
                //if (negativeY) randomDirection.y = randomDirection.y * -1;
                stepCount = 0;
            }
            golfBall.myRigidbody.velocity = randomDirection * walkSpeed;
            Debug.Log(randomDirection + " THIS IS THE RANDOM DIRECTION VECTOR");
        }
    }
}
