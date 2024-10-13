using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShootProjectile : MonoBehaviour
{
    //gets bullet prefab
    public Rigidbody2D bullet;
    //gets barrel position
    public Transform barrelEnd;
    //muzzle flash sprite
    public SpriteRenderer muzzleFlash;
    //soundPlayer
    private AudioSource audioPlayer;
    //gunshot sound
    public AudioClip gunshot;
    //timer
    private float timer = 10;
    //the launch force of the bullet being shot
    public float launchForce = -1200f;

    public float shootDelay;

    private GameObject myCamera;

    public WeaponClass defaultWeapon, currentWeapon;
    public SpriteRenderer currentGunSprite;

    private void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioPlayer = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
        UpdateWeapon(defaultWeapon); //Set starting weapon to player default weapon
    }
    void Update()
    {
        timer += Time.deltaTime;
        muzzleFlash.enabled = false;
        if (timer <= 0.1f)
        {
            muzzleFlash.enabled = true;
            myCamera.transform.position = new Vector3(0 + Random.Range(0, 0.08f), 0 + Random.Range(0, 0.08f), -10);
        }

    }
    public void ShootAction()
    {
        if (timer >= shootDelay)
        {
            Debug.Log("shoot");
            audioPlayer.pitch = Random.Range(0.9f, 1.1f);
            audioPlayer.PlayOneShot(gunshot, 1f);
            Rigidbody2D bulletInstance;
            bulletInstance = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody2D;
            bulletInstance.AddForce(-barrelEnd.up * launchForce);
            timer = 0;
        }
        
    }

    public void UpdateWeapon(WeaponClass newWeapon) //Called on picking up a new Weapon
    {
        //set weapon details to the currently equipped weapon
        currentWeapon = newWeapon;
        shootDelay = newWeapon.shootDelay;
        launchForce = newWeapon.launchForce;
        currentGunSprite.sprite = newWeapon.gunSprite;
    }
}
