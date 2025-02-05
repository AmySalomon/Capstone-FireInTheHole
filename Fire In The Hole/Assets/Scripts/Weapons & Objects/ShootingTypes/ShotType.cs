using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "WeaponClass/ShotType/Default")]

public class ShotType : ScriptableObject
{
    public BulletType bulletType; //how the bullet itself behaves
    public virtual void ShootBullets(Transform barrelEnd, float launchForce, float spreadAngle, GameObject playerShooter) //shoot a bullet
    {
        float shotSpread = Random.Range(-spreadAngle, spreadAngle);
        Rigidbody2D bulletInstance;
        bulletInstance = Instantiate(bulletType.bulletPrefab, barrelEnd.position, Quaternion.AngleAxis(shotSpread, barrelEnd.forward) * barrelEnd.rotation) as Rigidbody2D;
        bulletInstance.AddRelativeForce(Vector2.down * launchForce);
        bulletInstance.GetComponent<BulletManager>().bulletType = bulletType;
        bulletInstance.GetComponent<BulletManager>().playerShooter = playerShooter;
        bulletInstance.GetComponent<BulletManager>().UpdateBulletType();
    }
    
}
