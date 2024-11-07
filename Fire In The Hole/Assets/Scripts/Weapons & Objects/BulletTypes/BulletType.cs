using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "WeaponClass/BulletType/Default")]
public class BulletType : ScriptableObject
{
    public float deletionTime;
    public GameObject bullet;
    public Rigidbody2D bulletPrefab;
    public virtual void BulletCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "MainCamera" || collision.gameObject.tag == "Wall")
        {
            DeleteBullet();
        }

        if (collision.gameObject.tag == "Player")
        {
            DeleteBullet();
        }
    }
    
    public virtual void DeleteBullet()
    {
        Destroy(bullet);
    }
}
