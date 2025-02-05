using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public double timer = 5;
    public float radius = 2;
    public float explosiveForce = 2000;
    public LayerMask interactables;
    public Vector3 explosionPos;
    public string golfballTag = "Ball";
    private void Start()
    {
        Debug.Log("I'm alive");
        explosionPos = new Vector3(transform.position.x, transform.position.y, 0);
        PushGolfballs();

    }
    private void Update()
    {//go away after timer elapses
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void PushGolfballs()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, interactables);
        foreach(Collider2D hit in colliders)
        {
            if(!hit.CompareTag(golfballTag)) { return; }
            Debug.Log("Slay!");

            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("Extra Slay!");

                Vector2 forceDirection = transform.position - hit.transform.position;
                rb.AddForce(forceDirection.normalized * explosiveForce);
            }
        }
    }
}
