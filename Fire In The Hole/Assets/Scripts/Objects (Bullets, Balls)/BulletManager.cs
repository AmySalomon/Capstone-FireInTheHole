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

    void Start()
    {
        timer = 0;
        UpdateBulletType();

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
    //when the bullet collides with something, check the scriptableobject for what it should do
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bulletType.BulletCollision(collision, this.gameObject, playerShooter);

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
