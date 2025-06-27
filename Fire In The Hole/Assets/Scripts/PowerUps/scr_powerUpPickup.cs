using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_powerUpPickup : MonoBehaviour
{
    public scr_powerUpEffect[] powerUps;
    public scr_powerUpEffect powerUpEffect;
    public GameObject textPopup;
    public float duration = 10f;

    public Sprite ShieldIcon;
    public Sprite BurnDodgeIcon;
    public Sprite RicochetIcon;
    public Sprite SpeedupIcon;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) //if the gameobject is the player, update players weapon, then remove pickup
        {
            //collision.gameObject.GetComponent<scr_powerUpManager>().Powerup
            collision.gameObject.GetComponentInChildren<scr_powerUpManager>().activePowerUp = powerUpEffect;
            collision.gameObject.GetComponentInChildren<scr_powerUpManager>().isTemporary = true;
            collision.gameObject.GetComponentInChildren<scr_powerUpManager>().duration = duration;
            collision.gameObject.GetComponentInChildren<PlayerStatTracker>().UpdatePowerUpsGained();
            var newPopup = Instantiate(textPopup, collision.transform.position, Quaternion.identity);
            newPopup.GetComponent<TextPopup>().weaponPickup = powerUpEffect.name;
            //var newPopup = Instantiate(textPopup, collision.transform.position, transform.rotation);
            //newPopup.GetComponent<TextPopup>().weaponPickup = chosenWeapon.name;
            Debug.Log("Now applying powerup to " + collision.gameObject.GetComponentInChildren<PlayerStatTracker>().myConfig.PlayerSprite.name);

            Destroy(this.gameObject);

        }
    }

    private void Start()
    {
        powerUpEffect = powerUps[Random.Range(0, powerUps.Length)];
        if (powerUpEffect.name == "PU_speed Boost")
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = SpeedupIcon;
        }
        if (powerUpEffect.name == "PU_bouncyBullet")
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = RicochetIcon;
        }
        if (powerUpEffect.name == "PU_burnDash")
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = BurnDodgeIcon;
        }
        if (powerUpEffect.name == "PU_shield")
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = ShieldIcon;
        }
    }
}

