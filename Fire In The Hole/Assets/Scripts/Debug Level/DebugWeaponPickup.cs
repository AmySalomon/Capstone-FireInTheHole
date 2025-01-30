using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeaponPickup : MonoBehaviour
{
    public WeaponClass[] weapon;
    public WeaponClass chosenWeapon;
    public SpriteRenderer weaponSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if the gameobject is the player, update players weapon, then remove pickup
        {
            collision.gameObject.GetComponent<ShootProjectile>().UpdateWeapon(chosenWeapon);
        }
    }

    private void Start()
    {
        //Pick a random weapon from the list, and show it in the powerup visual
        chosenWeapon = weapon[Random.Range(0, weapon.Length)];
        weaponSprite.sprite = chosenWeapon.gunSprite;
    }
}
