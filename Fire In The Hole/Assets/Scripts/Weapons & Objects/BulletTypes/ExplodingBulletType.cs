using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponClass/BulletType/Exploding")]
[System.Serializable]
public class ExplodingBulletType : BulletType
{
    public GameObject explosion;
    public float explosionRadius;
    //public Transform explosionCenter;


    public override void DeleteBullet()
    {
        GameObject explosionInstance = Instantiate(explosion);
        explosionInstance.transform.position = bullet.transform.position;
        base.DeleteBullet();
    }
    
}
