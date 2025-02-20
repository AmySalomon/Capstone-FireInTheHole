using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_scattershotChild : MonoBehaviour
{
    public bool active;
    public GameObject golfball;
    private GameObject player;
    //public scr_PU_scattershot scrScattershot;
    public float spawnOffset = 0.5f;
    public scr_golfBall scr_golfBall;
    public Rigidbody2D rbGolfBall;
    public Outline gbOutline;

    private bool playerGrabbed = false;
    private scr_meleeSwing MeleeSwing;
    public bool clone = false;

    void Start()
    {
        active = true;
    }

    void Update()
    {
        if (scr_golfBall.playerHitter != null && scr_golfBall.balltype == 2 && active == true) //checks if ball was hit by a player and if the power up is active (needs changing for ball type)
        {
            HitCheck();
            active = false;
        }
    }

    public void HitCheck()
    {
        
        if (scr_golfBall.playerHitter != null && clone == false)
        {
            if (playerGrabbed == false)
            {
                player = scr_golfBall.playerHitter;
                MeleeSwing = player.GetComponentInChildren<scr_meleeSwing>();
                Debug.Log(MeleeSwing);
                Debug.Log(player);
                playerGrabbed =true;
            }

            if (MeleeSwing != null)
            {
                if (scr_golfBall.playerHitter != null && active == true)
                {
                    Vector2 originalPosition = rbGolfBall.transform.position;
                    Vector2 perpendicular = Vector2.Perpendicular(MeleeSwing.forceDirection).normalized * spawnOffset;
                    Vector2 leftSpawnPosition = originalPosition - perpendicular;
                    Vector2 rightSpawnPosition = originalPosition + perpendicular;

                    GameObject Rgolfball;
                    GameObject Lgolfball;

                    Rgolfball = Instantiate(golfball, rightSpawnPosition, Quaternion.identity);

                    Rgolfball.GetComponent<scr_golfBall>().playerHitter = scr_golfBall.playerHitter;
                    Rgolfball.GetComponent<Outline>().OutlineColor = gbOutline.OutlineColor;

                    Lgolfball = Instantiate(golfball, leftSpawnPosition, Quaternion.identity);
                    Lgolfball.GetComponent<scr_golfBall>().playerHitter = scr_golfBall.playerHitter;
                    Lgolfball.GetComponent<Outline>().OutlineColor = gbOutline.OutlineColor;

                    Rgolfball.GetComponent<scr_scattershotChild>().clone = true;
                    Lgolfball.GetComponent<scr_scattershotChild>().clone = true;

                    Rigidbody2D rightRb = Rgolfball.GetComponent<Rigidbody2D>();
                    Rigidbody2D leftRb = Lgolfball.GetComponent<Rigidbody2D>();

                    float dynamicForce = MeleeSwing.currentSwingForce;

                    float angleVariation = 10f;

                    Vector2 rightForceDirection = Quaternion.Euler(0, 0, angleVariation) * MeleeSwing.forceDirection;

                    Vector2 leftForceDirection = Quaternion.Euler(0, 0, -angleVariation) * MeleeSwing.forceDirection;


                    if (rightRb != null)
                    {
                        rightRb.AddForce(rightForceDirection.normalized * dynamicForce / 2);
                    }

                    if (leftRb != null)
                    {
                        leftRb.AddForce(leftForceDirection.normalized * dynamicForce / 2);
                    }
                    active = false;
                    Debug.Log("Scattershot!");
                }
            }
            //var MeleeSwing = player.GetComponent<scr_meleeSwing>();
            //var MeleeSwingTemp = 

            

        }
    }
}
