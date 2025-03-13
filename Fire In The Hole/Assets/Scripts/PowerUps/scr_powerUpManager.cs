using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static scr_powerUpManager;

public class scr_powerUpManager : MonoBehaviour
{
    public GameObject playerObj;

    [System.Serializable]
    public class PowerUp
    {
        public string name; //what is the name of the power up?
        public scr_powerUpEffect effect; //scriptable effect object
    }

    public float duration; //how long does the effect last?
    public bool isTemporary; //is your effect temporary?
    private Coroutine activeCoroutine;//storing the coroutine

    //public List<PowerUp> activePowerUps = new List<PowerUp>(); //List of currently active powerups
    public scr_powerUpEffect activePowerUp;
    public scr_powerUpEffect tempActivePowerUp;
    public PlayerDeath PlayerDeath;
    public bool isDead = false;

    [Header("Testing")]
    public PowerUp testPowerUp;

    void Update()
    {
        isDead = PlayerDeath.playerIsDead;
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
            tempActivePowerUp = activePowerUp;
            activePowerUp = null;
        }
    }

    public void AddPowerUp(scr_powerUpEffect newPowerUp, GameObject player)
    {
        if (player == null) return;

        if (tempActivePowerUp != null)
        {
            tempActivePowerUp.RemoveEffect(player);
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }
        }

        newPowerUp.ApplyEffect(player);
        tempActivePowerUp = newPowerUp;

        if (isTemporary)
        {
            activeCoroutine = StartCoroutine(RemovePowerUpAfterDuration(newPowerUp, player));
        }
    }

    public void RemovePowerUp()
    {
        if (tempActivePowerUp != null)
        {
            tempActivePowerUp.RemoveEffect(playerObj);
        }
        //tempActivePowerUp = null;
    }

    private System.Collections.IEnumerator RemovePowerUpAfterDuration(scr_powerUpEffect powerUp, GameObject player)
    {
        yield return new WaitForSeconds(duration);

        powerUp.RemoveEffect(player);
        activeCoroutine = null;
        Debug.Log("Powerup removed!");

    }
}
