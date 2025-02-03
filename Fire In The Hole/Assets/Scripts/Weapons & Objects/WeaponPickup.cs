using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponClass[] weapon;
    public WeaponClass chosenWeapon;
    public SpriteRenderer weaponSprite;
    public GameObject textPopup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if the gameobject is the player, update players weapon, then remove pickup
        {
            collision.gameObject.GetComponent<ShootProjectile>().UpdateWeapon(chosenWeapon);
            var newPopup = Instantiate(textPopup, collision.transform.position, transform.rotation);
            newPopup.GetComponent<TextPopup>().weaponPickup = chosenWeapon.name;
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //Pick a random weapon from the list, and show it in the powerup visual
        chosenWeapon = weapon[Random.Range(0, weapon.Length)];
        Debug.Log("weapon chosen is " + chosenWeapon.name);
        weaponSprite.sprite = chosenWeapon.gunSprite;
    }
}
