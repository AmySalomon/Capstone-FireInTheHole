using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponClass weapon;
    public SpriteRenderer weaponSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if the gameobject is the player, update players weapon, then remove pickup
        {
            collision.gameObject.GetComponent<ShootProjectile>().UpdateWeapon(weapon);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        weaponSprite.sprite = weapon.gunSprite;
    }
}
