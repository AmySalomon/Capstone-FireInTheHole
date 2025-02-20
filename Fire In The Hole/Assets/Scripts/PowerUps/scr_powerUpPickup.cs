using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_powerUpPickup : MonoBehaviour
{
    public scr_powerUpEffect[] powerUps;
    public scr_powerUpEffect powerUpEffect;
    public SpriteRenderer powerUpSprite;
    public GameObject textPopup;
    public float duration = 10f;
    
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if the gameobject is the player, update players weapon, then remove pickup
        {
            //collision.gameObject.GetComponent<scr_powerUpManager>().Powerup
            collision.gameObject.GetComponentInChildren<scr_powerUpManager>().activePowerUp = powerUpEffect;
            collision.gameObject.GetComponentInChildren<scr_powerUpManager>().isTemporary = true;
            collision.gameObject.GetComponentInChildren<scr_powerUpManager>().duration = duration;
            var newPopup = Instantiate(textPopup, collision.transform.position, transform.rotation);
            newPopup.GetComponent<TextPopup>().weaponPickup = powerUpEffect.name;
            //var newPopup = Instantiate(textPopup, collision.transform.position, transform.rotation);
            //newPopup.GetComponent<TextPopup>().weaponPickup = chosenWeapon.name;
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        powerUpEffect = powerUps[Random.Range(0, powerUps.Length)];
    }

}
