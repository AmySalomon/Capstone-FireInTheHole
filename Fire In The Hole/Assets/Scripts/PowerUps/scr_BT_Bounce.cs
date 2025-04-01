using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BT_Bounce : MonoBehaviour //Bouncing betty ball type
{
    public GameObject explosionPrefab;
    private int explosionCount = 0;
    //private int maxExplosions = 3;
    public bool bounceEnabled = false;

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (/*explosionCount < maxExplosions && */collision.gameObject.CompareTag("Wall") && bounceEnabled == true)
        {
            Explode();
            explosionCount++;
        }
    }

    private void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
