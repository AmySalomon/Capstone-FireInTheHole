using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_golfBall : MonoBehaviour
{
    public Transform player;
    public GameObject directionArrowPrefab;
    private float interactionRange;
    public string golfHoleTag = "GolfHole";

    private GameObject directionArrowInstance;
    private bool isPlayerInRange = false;
    private scr_meleeSwing playerGolfSwing;

    bool tempPlayerCheck = false;

    private void Start()
    {
        //put initializing code here and delete Temp

    }

    private void Update()
    {
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
            Destroy(gameObject);
            Debug.Log("Ball entered the hole! Destroying ball.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}