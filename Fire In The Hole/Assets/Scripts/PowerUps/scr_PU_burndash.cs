using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BurnDash")]
public class scr_PU_burndash : scr_powerUpEffect
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponentInChildren<Dash>().BurningDash = true;
    }

    public override void RemoveEffect(GameObject player)
    {
        player.GetComponentInChildren<Dash>().BurningDash = false;
    }
}
