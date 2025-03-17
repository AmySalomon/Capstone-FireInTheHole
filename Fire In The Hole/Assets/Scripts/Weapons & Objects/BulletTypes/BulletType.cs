using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
[CreateAssetMenu(menuName = "WeaponClass/BulletType/Default")]
public class BulletType : ScriptableObject
{
    public float deletionTime; //how long the bullet lasts for
    public Rigidbody2D bulletPrefab; //the gameobject that's being spawned
    public virtual void BulletCollision(Collision2D collision, GameObject bullet, GameObject playerShooter)
    {
        if (collision.gameObject.tag == "MainCamera" || collision.gameObject.tag == "Wall")
        {
            DeleteBullet(bullet, playerShooter);
        }

        if (collision.gameObject.tag == "Player")
        {
            DeleteBullet(bullet, playerShooter);
        }
    }
    
    public virtual void DeleteBullet(GameObject bullet, GameObject playerShooter)
    {
        Destroy(bullet);
    }
}
