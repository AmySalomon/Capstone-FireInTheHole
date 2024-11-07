using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "WeaponClass/ShotType/Default")]

public class ShotType : ScriptableObject
{
    public BulletType bulletType; //how the bullet itself behaves
    public virtual void ShootBullets(Transform barrelEnd, float launchForce) //shoot a bullet
    {
        Rigidbody2D bulletInstance;
        bulletInstance = Instantiate(bulletType.bulletPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody2D;
        bulletInstance.AddForce(-barrelEnd.up * launchForce);
        bulletInstance.GetComponent<BulletManager>().bulletType = bulletType;
    }
    
}
