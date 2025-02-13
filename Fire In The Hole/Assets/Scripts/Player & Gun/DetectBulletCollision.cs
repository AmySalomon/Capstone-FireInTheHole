using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBulletCollision : MonoBehaviour
{
    public PlayerDeath deathManager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //gets the vector direction from which the player was killed to use for the death animation
            if (collision != null) deathManager.deathDirection = collision.transform.position - transform.position;
            deathManager.Died();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //gets the vector direction from which the player was killed to use for the death animation
            if (collision != null) deathManager.deathDirection = collision.transform.position - transform.position;
            deathManager.Died();
        }
    }
}
