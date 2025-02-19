using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_powerUpManager : MonoBehaviour
{
    public GameObject playerObj;
    public bool isBomb = false; //bombPower up enabled?

    [System.Serializable]
    public class PowerUp
    {
        public string name; //what is the name of the power up?

 
        public scr_powerUpEffect effect; //scriptable effect object
    }

    public float duration; //how long does the effect last?
    public bool isTemporary; //is your effect temporary?

    //public List<PowerUp> activePowerUps = new List<PowerUp>(); //List of currently active powerups
    public scr_powerUpEffect activePowerUp;

    [Header("Testing")]
    public PowerUp testPowerUp;

    void Update()
    {
        //have addpowerup(collided powerup, player obj)
        //press 1 to test powerup
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(testPowerUp + " up enabled");
            //AddPowerUp(testPowerUp, playerObj);
        }

        if (activePowerUp != null)
        {
            AddPowerUp(activePowerUp, playerObj);
            activePowerUp = null;
        }
    }

    public void AddPowerUp(scr_powerUpEffect newPowerUp, GameObject player)
    {
        if (player == null) return;

        //aapply powerup
        newPowerUp.ApplyEffect(player);

        if (isTemporary)
        {
            //activePowerUps.Add(newPowerUp);
            StartCoroutine(RemovePowerUpAfterDuration(newPowerUp, player));
        }
    }

    private System.Collections.IEnumerator RemovePowerUpAfterDuration(scr_powerUpEffect powerUp, GameObject player)
    {
        yield return new WaitForSeconds(duration);

        if (powerUp != null && player != null)
        {
            powerUp.RemoveEffect(player);
            Debug.Log("Powerup removed!");
        }

    }
}
