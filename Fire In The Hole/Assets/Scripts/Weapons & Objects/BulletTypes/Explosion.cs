using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public double timer = 0.25;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("They go boom");
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<DetectBulletCollision>().deathManager.Died();
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            //GetComponent<BulletManager>().DestroyBullet();
            Destroy(this.gameObject);
        }
    }
}
