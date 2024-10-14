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
    private float shotTimer = 10; //timer to track shoot bullets
    private float reloadTimer, reloadTimerMax; //timer to track reloading magazine
    //the launch force of the bullet being shot
    public float launchForce = -1200f;

    public float shootDelay; //time between shots
    public float screenShake; //how hard screen shakes
    public int ammoMax; //how much ammo is in each magazine
    public int ammoCurrent; //how much ammo the player currently has left
    public int magazineCount; //how many magazines the player has
    public bool reloading = false; //is the player reloading right now
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
        shotTimer += Time.deltaTime;
        if (reloading)
        {
            reloadTimer -= Time.deltaTime;
            if(reloadTimer<= 0)
            {
                ReloadComplete();
            }
        }
        muzzleFlash.enabled = false;
        //Screenshake
        if (shotTimer <= 0.1f)
        {
            muzzleFlash.enabled = true;
            myCamera.transform.position = new Vector3(0 + Random.Range(0, screenShake), 0 + Random.Range(0, screenShake), -10);
        }

    }
    public void ShootAction()
    {
        if (reloading) { return; } //if player is reloading, ignore input
        if (shotTimer >= shootDelay)
        {
            if(ammoCurrent <= 0) //check if player needs to reload / use new magazine
            {
                Reload();
                return;
            }
            Debug.Log("shoot");
            audioPlayer.pitch = Random.Range(0.9f, 1.1f);
            audioPlayer.PlayOneShot(gunshot, 1f);
            Rigidbody2D bulletInstance;
            bulletInstance = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody2D;
            bulletInstance.AddForce(-barrelEnd.up * launchForce);
            shotTimer = 0;
            ammoCurrent--;
            if(ammoCurrent <=0 && magazineCount <= 0) //when the player fully exhausts all bullets and weapon magazines, switch to default weapon
            {
                UpdateWeapon(defaultWeapon);
            }
        }
        
    }

    public void Reload()
    {
        reloading = true;
        reloadTimer = reloadTimerMax;
    }

    public void ReloadComplete() //when reloading complete, remove a magainze and refill player ammo
    {
        reloading = false;
        magazineCount--;
        ammoCurrent = ammoMax;
    }
    public void UpdateWeapon(WeaponClass newWeapon) //Called on picking up a new Weapon
    {
        //set weapon details to the currently equipped weapon
        currentWeapon = newWeapon;
        shootDelay = newWeapon.shootDelay;
        launchForce = newWeapon.launchForce;
        currentGunSprite.sprite = newWeapon.gunSprite;
        screenShake = newWeapon.screenShake;
        ammoMax = newWeapon.ammoMax;
        ammoCurrent = ammoMax;
        magazineCount = newWeapon.magazineCount;
        reloadTimerMax = newWeapon.reloadSpeed;
    }
}
