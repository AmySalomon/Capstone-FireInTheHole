using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A Script that teleports objects on contact to a set destination
public class PortalHazard : MonoBehaviour
{
    public GameObject portalDestination;
    public  List<GameObject> collisionList = new List<GameObject>();
    //soundPlayer
    private AudioSource audioPlayer;

    //Portal audio
    public AudioClip portalWoosh;

    //Portal Position Change variables
    public float portalTimerMin = 15, portalTimerMax = 30, portalTimerCurrent;
    public static int portalLocationChosen;
    public static bool portalChosen = false;
    public List<GameObject> portalLocations = new List<GameObject>();

    //Start
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger2D detected: colliding with "+collision.name);

        //if object hasn't teleported recently, teleport it and prevent it from using the portal for cooldown time
        if (!collisionList.Contains(collision.gameObject))
        {
            PortalLockout(collision.gameObject);
            Vector3 spawnOffset = collision.gameObject.transform.position - gameObject.transform.position;
            spawnOffset.z = 0;
            spawnOffset.y *= -1;
            Debug.Log("spawnOffset is " + spawnOffset);
            collision.attachedRigidbody.position = portalDestination.transform.position-spawnOffset;

            //Play Portal Sound
            audioPlayer.pitch = Random.Range(0.9f, 1.1f);
            audioPlayer.PlayOneShot(portalWoosh, 1f);
        }

    }

    //add objects that has teleported to both lockout lists to prevent accidental teleporting
    private void PortalLockout(GameObject gameObject)
    {
        collisionList.Add(gameObject);
        portalDestination.GetComponent<PortalHazard>().collisionList.Add(gameObject);
    }

    //players must leave the portal hitbox in order to reenter a portal
    private void OnTriggerExit2D(Collider2D collision) 
    {
        Debug.Log("Player left portal "+ this.gameObject.name);
        collisionList.Remove(collision.gameObject);
    }

    
}
