using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "WeaponClass/ShotType/Multishot")]
public class MultishotShotType : ShotType
{
    public int bulletsPerShot;
    public float bulletOffset; // angle between each bullet
    public float startingAngle; // angle of the highest bullet
    public override void ShootBullets(Transform barrelEnd, float launchForce) //shoot bullets in a spread
    {
        float currentAngle = startingAngle;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Rigidbody2D bulletInstance;
            bulletInstance = Instantiate(bulletType.bulletPrefab, barrelEnd.position, Quaternion.AngleAxis(currentAngle, barrelEnd.forward) * barrelEnd.rotation) as Rigidbody2D;
            bulletInstance.AddForce(Quaternion.AngleAxis(-currentAngle, -barrelEnd.forward) * -barrelEnd.up * launchForce);
            currentAngle -= bulletOffset;
            bulletInstance.GetComponent<BulletManager>().bulletType = bulletType;
        }
    }
    
}
