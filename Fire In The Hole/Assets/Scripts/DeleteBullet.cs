using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteBullet : MonoBehaviour
{
    private float timer;
    private float resetTimer;
    public float deletionTime = 5;

    private bool hasKilled = false;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= deletionTime && !hasKilled) Destroy(gameObject);

        if (hasKilled)
        {
            resetTimer += Time.deltaTime;

            if (resetTimer >= 3)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "MainCamera" || collision.gameObject.tag == "Wall")
        {
            if (!hasKilled) Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            hasKilled = true;
            Destroy(collision.gameObject);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
