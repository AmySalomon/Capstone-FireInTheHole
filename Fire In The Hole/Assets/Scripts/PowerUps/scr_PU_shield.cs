using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Shield")]
public class scr_PU_shield : scr_powerUpEffect
{
    public override void ApplyEffect(GameObject player)
    {
        var playerDeath = player.GetComponentInParent<PlayerDeath>();
        if (playerDeath != null)
        {
            playerDeath.shieldActive = true;
        }
        else { Debug.Log("Shield Null"); }
    }

    public override void RemoveEffect(GameObject player)
    {

    }

    /*
     * in PlayerDeath.cs
     * void (death)
     * if (shieldactive == false)
     *         die
     * 
     * in bullet collision check
     * if (collision == bullet && shieldactive == true)
     * shieldactive = false
     * 
     * shieldChargeTimer = 5 seconds
     * 
     * if (shieldactive == false)
     *      if (timer.deltatime >= shieldChargeTimer)
     *      shieldactive = true
     * 
     * 
     * 
     * player needs to be able to enable it and disable its functionality at will due to it being a power up, cant conflict with existing death code for ohko so be careful
     */
}
