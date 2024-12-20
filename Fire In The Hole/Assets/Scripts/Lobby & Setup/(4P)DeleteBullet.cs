using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourPlayerDeleteBullet : MonoBehaviour
{
    private float timer;
    public float deletionTime = 5;


    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= deletionTime) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "MainCamera" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
