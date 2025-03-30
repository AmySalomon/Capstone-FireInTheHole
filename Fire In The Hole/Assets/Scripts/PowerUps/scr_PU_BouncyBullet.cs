using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BouncyBullet")]
public class scr_PU_BouncyBullet : scr_powerUpEffect
{
    public override void ApplyEffect(GameObject player)
    {
        var playerGun = player.GetComponent<ShootProjectile>();
        if (playerGun != null)
        {
            playerGun.isBounce = true;
        }
    }

    public override void RemoveEffect(GameObject player)
    {
        var playerGun = player.GetComponent<ShootProjectile>();
        if (playerGun != null)
        {
            playerGun.isBounce = false;
        }
    }
}


