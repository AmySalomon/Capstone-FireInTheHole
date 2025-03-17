using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfballChecker : MonoBehaviour
{
    public GameObject bullet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            bullet.GetComponent<BulletManager>().GolfBallOffset(collision);
        }
    }

    
}
