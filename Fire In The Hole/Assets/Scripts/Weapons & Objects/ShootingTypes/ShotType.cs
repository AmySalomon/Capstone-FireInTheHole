using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "WeaponClass/ShotType/Default")]

public class ShotType : ScriptableObject
{
    public float launchForce;
    public Transform barrelEnd;
    public BulletType bulletType;
    public virtual void ShootBullets()
    {
        Rigidbody2D bulletInstance;
        bulletInstance = Instantiate(bulletType.bulletPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody2D;
        bulletInstance.AddForce(-barrelEnd.up * launchForce);
        bulletInstance.GetComponent<BulletManager>().bulletType = bulletType;
    }
    
}
