using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BombBall")]
public class scr_PU_Bombball : scr_powerUpEffect
{
    public override void ApplyEffect(GameObject player)
    {
        var meleeswing = player.GetComponent<scr_meleeSwing>();
        meleeswing.balltype = 1;
    }

    public override void RemoveEffect(GameObject player)
    {
        var meleeswing = player.GetComponent<scr_meleeSwing>();
        meleeswing.balltype = 0;
    }
}
