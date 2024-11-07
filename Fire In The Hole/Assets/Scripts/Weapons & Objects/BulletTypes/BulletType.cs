using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "WeaponClass/BulletType/Default")]
public class BulletType : ScriptableObject
{
    public float deletionTime;
    public Rigidbody2D bulletPrefab;
    public virtual void BulletCollision(Collision2D collision, GameObject bullet)
    {
        if (collision.gameObject.tag == "MainCamera" || collision.gameObject.tag == "Wall")
        {
            DeleteBullet(bullet);
        }

        if (collision.gameObject.tag == "Player")
        {
            DeleteBullet(bullet);
        }
    }
    
    public virtual void DeleteBullet(GameObject bullet)
    {
        Destroy(bullet);
    }
}
