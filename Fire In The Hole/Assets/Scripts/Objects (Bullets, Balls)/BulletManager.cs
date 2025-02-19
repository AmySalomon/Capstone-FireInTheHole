using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public BulletType bulletType;
    public float timer;
    public float deletionTime = 5;
    public GameObject playerShooter;
    public GameObject wallHitImpact;
    private Rigidbody2D myRigidbody;
    public PhysicsMaterial2D bounceMaterial;

    [HideInInspector] public bool canBounce = false;

    private Vector2 lastVelocity;
    void Start()
    {
        timer = 0;
        UpdateBulletType();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (canBounce)
        {
            myRigidbody.sharedMaterial = bounceMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if bullet lingers too long, destroy it
        timer += Time.deltaTime;
        if (timer >= deletionTime)
        {
            DestroyBullet(this.gameObject);
        }
    }

    private void LateUpdate()
    {
        lastVelocity = myRigidbody.velocity;
    }
    //when the bullet collides with something, check the scriptableobject for what it should do
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (canBounce && collision.gameObject.tag == "Wall")
        {
            transform.rotation = Quaternion.FromToRotation(-Vector2.up, Vector2.Reflect(lastVelocity.normalized, collision.GetContact(0).normal.normalized));
            canBounce = false;
        }

        else
        {
            bulletType.BulletCollision(collision, this.gameObject, playerShooter);
        }
        if (collision.gameObject.tag == "Wall")
        {
            var hitImpact = Instantiate(wallHitImpact, transform.position, Quaternion.identity);
            hitImpact.transform.rotation = Quaternion.FromToRotation(Vector2.right, collision.GetContact(0).normal.normalized);
        }

    }

    public void UpdateBulletType()//Get all the info of the BulletType
    {
        bulletType = ScriptableObject.Instantiate(bulletType);
        deletionTime = bulletType.deletionTime;
    }
    public void DestroyBullet(GameObject bullet)
    {
        bulletType.DeleteBullet(this.gameObject, playerShooter);
    }
}
