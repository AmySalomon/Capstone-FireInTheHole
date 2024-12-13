using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public ShotType[] shotTypes;
    public ShotType chosenShotType;
   // public SpriteRenderer powerupSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if the gameobject is the player, update players weapon, then remove pickup
        {
            collision.gameObject.GetComponent<ShootProjectile>().UpdateShotType(chosenShotType);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //Pick a random powerup from the list, and show it in the powerup visual
        //chosenShotType = shotTypes[Random.Range(0, shotTypes.Length)];
       // Debug.Log("weapon chosen is " + chosenShotTypes);
       // powerupSprite.sprite = chosenShotTypes.gunSprite;
    }
}
