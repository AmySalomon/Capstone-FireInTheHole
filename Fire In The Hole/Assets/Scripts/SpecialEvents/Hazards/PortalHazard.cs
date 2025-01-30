using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHazard : MonoBehaviour
{
    public GameObject portalDestination;
    public float cooldown = 4;
    public float cooldownMax = 4;
    public static List<GameObject> collisionList = new List<GameObject>();

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger2D detected: colliding with "+collision.name);

        //if object hasn't teleported recently, teleport it and prevent it from using the portal for cooldown time
        if (!collisionList.Contains(collision.gameObject))
        {
            StartCoroutine(PortalLockout(collision.gameObject));
            Vector3 spawnOffset = collision.gameObject.transform.position - gameObject.transform.position;
            spawnOffset.z = 0;
            spawnOffset.y *= -1;
            Debug.Log("spawnOffset is " + spawnOffset);
            collision.attachedRigidbody.position = portalDestination.transform.position-spawnOffset;
        }

    }

    //add objects that has teleported recently to the lockout list, then remove them after a cooldown
    IEnumerator PortalLockout(GameObject gameObject)
    {
        collisionList.Add(gameObject);
        yield return new WaitForSeconds(cooldown);
        collisionList.Remove(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("trigger detected: colliding with " + other.name);
    //    if (cooldown <= 0)
    //    {
    //        other.attachedRigidbody.position = portalDestination.transform.position;
    //        cooldown = cooldownMax;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("collision detected: colliding with " + collision.transform.name);
    //    if (cooldown <= 0)
    //    {
    //        collision.transform.position = portalDestination.transform.position;
    //        cooldown = cooldownMax;
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("collision2D detected: colliding with " + collision.transform.name);
    //    if (cooldown <= 0)
    //    {
    //        collision.transform.position = portalDestination.transform.position;
    //        cooldown = cooldownMax;
    //    }
    //}
}
