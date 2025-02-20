using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBoostEffect")]
public class scr_PU_speedBoost : scr_powerUpEffect
{
    public float speedMultiplier = 4f;
    //public PlayerMovement playerMovement;
    public float originalSpeed = 3;

    public override void ApplyEffect(GameObject player)
    {
        var PlayerMovement = player.GetComponent<PlayerMovement>();
        if (PlayerMovement != null)
        {
            PlayerMovement.currentMoveSpeed = originalSpeed;
            PlayerMovement.currentMoveSpeed *= speedMultiplier;
            //Debug.Log("speedBoosted!");
        }
    }

    public override void RemoveEffect(GameObject player)
    {
        var PlayerMovement = player.GetComponent<PlayerMovement>();
        if (PlayerMovement != null)
        {
            PlayerMovement.currentMoveSpeed = originalSpeed;
        }
    }
}
