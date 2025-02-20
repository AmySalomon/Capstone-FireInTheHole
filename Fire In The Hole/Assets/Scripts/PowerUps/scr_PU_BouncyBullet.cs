using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BouncyBullet")]
public class scr_PU_BouncyBullet : scr_powerUpEffect
{
    public override void ApplyEffect(GameObject player)
    {
        //var bullet = player.GetComponentInChildren<ShootProjectile>().bullet;
        //bullet.GetComponent<BulletManager>().canBounce = true;
        player.GetComponentInChildren<ShootProjectile>().isBounce = true;
    }

    public override void RemoveEffect(GameObject player)
    {
       // var bullet = player.GetComponentInChildren<ShootProjectile>().bullet;
       // bullet.GetComponent<BulletManager>().canBounce = false;
       player.GetComponentInChildren<ShootProjectile>().isBounce = false;
    }
}
