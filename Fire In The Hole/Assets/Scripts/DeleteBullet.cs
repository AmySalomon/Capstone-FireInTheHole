using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBullet : MonoBehaviour
{
    private float timer;
    public float deletionTime = 5;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= deletionTime) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.name == "Main Camera") Destroy(gameObject);
    }
}
