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
    public override void DeleteBullet(GameObject bullet)
    {
        GameObject explosionInstance = Instantiate(explosion);
        explosionInstance.transform.position = bullet.transform.position;
        base.DeleteBullet(bullet);
    }
    
}
