using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponClass/BulletType/Exploding")]
[System.Serializable]
public class ExplodingBulletType : BulletType
{
    public GameObject explosion;
    public float explosionRadius;

    //When the bullet would despawn, create a harmful explosion in its place (then despawn)
    public override void BulletCollision(Collision2D collision, GameObject bullet, GameObject playerShooter)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Found one! Now trying to self delete");
            DeleteBullet(bullet, playerShooter);
        }
        base.BulletCollision(collision, bullet, playerShooter);
        
    }

    public override void DeleteBullet(GameObject bullet, GameObject playerShooter)
    {
        Debug.Log("making explosion?");
        GameObject explosionInstance = Instantiate(explosion);
        explosionInstance.transform.position = bullet.transform.position;
        explosionInstance.TryGetComponent<Explosion>(out Explosion scriptThing);
        scriptThing.playerShooter = playerShooter;
        base.DeleteBullet(bullet, playerShooter);
    }
    
}
