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
        public float duration; //how long does the effect last?
        public bool isTemporary; //is your effect temporary?
        public scr_powerUpEffect effect; //scriptable effect object
    }

    public List<PowerUp> activePowerUps = new List<PowerUp>(); //List of currently active powerups

    [Header("Testing")]
    public PowerUp testPowerUp;

    void Update()
    {
        //press 1 to test powerup
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(testPowerUp + " up enabled");
            AddPowerUp(testPowerUp, playerObj);
        }
    }

    public void AddPowerUp(PowerUp newPowerUp, GameObject player)
    {
        if (player == null) return;

        //aapply powerup
        newPowerUp.effect.ApplyEffect(player);

        if (newPowerUp.isTemporary)
        {
            activePowerUps.Add(newPowerUp);
            StartCoroutine(RemovePowerUpAfterDuration(newPowerUp, player));
        }
    }

    private System.Collections.IEnumerator RemovePowerUpAfterDuration(PowerUp powerUp, GameObject player)
    {
        yield return new WaitForSeconds(powerUp.duration);

        //remove effect
        powerUp.effect.RemoveEffect(player);
        activePowerUps.Remove(powerUp);
    }
}
