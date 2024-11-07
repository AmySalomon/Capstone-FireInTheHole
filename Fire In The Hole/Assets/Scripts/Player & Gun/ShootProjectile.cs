using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
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
    //timer for shooting bullets
    private float shootTimer = 10;
    //timer for reloading with new Magazine
    private float reloadTimer, reloadTimerMax;
    //the launch force of the bullet being shot
    public float launchForce = -1200f;

    public float shootDelay; //time between shots
    public float screenShake; //how hard screen shakes
    public int ammoMax; //how much ammo is in each magazine
    public int ammoCurrent; //how much ammo the player currently has left
    public int magazineCount; //how many magazines the player has
    private GameObject myCamera;

    public WeaponClass defaultWeapon, currentWeapon;
    public SpriteRenderer currentGunSprite;
    public bool reloading = false;
    [HideInInspector] public bool isTryingToShoot;

    [SerializeField] private GameObject reloadingText;
    private void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioPlayer = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
        reloadingText.SetActive(false);
        UpdateWeapon(defaultWeapon); //Set starting weapon to player default weapon
    }
    void Update()
    {
        shootTimer += Time.deltaTime;
        muzzleFlash.enabled = false;
        if (reloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                ReloadComplete();
            }
        }
        //Screenshake
        if (shootTimer <= 0.1f)
        {
            muzzleFlash.enabled = true;
            myCamera.transform.position = new Vector3(0 + Random.Range(0, screenShake), 0 + Random.Range(0, screenShake), -10);
        }

        if (isTryingToShoot)
        {
            ShootAction();
        }
    }
    public void ShootAction()
    {
        if (reloading) { return; }
        if (shootTimer >= shootDelay)
        {
            if(ammoCurrent <= 0)
            {
                StartReloading();
                return;
            }
            Debug.Log("shoot");
            audioPlayer.pitch = Random.Range(0.9f, 1.1f);
            audioPlayer.PlayOneShot(gunshot, 1f);
            if (currentWeapon.shotgun)
            { //shotgun shoots three bullets, slightly down, forward, and slightly up
                Rigidbody2D bulletInstance;
                bulletInstance = Instantiate(bullet, barrelEnd.position, Quaternion.AngleAxis(-15, barrelEnd.forward)*barrelEnd.rotation) as Rigidbody2D;
                bulletInstance.AddForce(Quaternion.AngleAxis(15, -barrelEnd.forward)*-barrelEnd.up * launchForce);
                Rigidbody2D bulletInstanceTwo;
                bulletInstanceTwo = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody2D;
                bulletInstanceTwo.AddForce(-barrelEnd.up * launchForce);
                Rigidbody2D bulletInstanceThree;
                bulletInstanceThree = Instantiate(bullet, barrelEnd.position, Quaternion.AngleAxis(15, barrelEnd.forward) *barrelEnd.rotation) as Rigidbody2D;
                bulletInstanceThree.AddForce(Quaternion.AngleAxis(-15, -barrelEnd.forward) * -barrelEnd.up * launchForce);
                ammoCurrent -= 2;
            }
            else
            {
                Rigidbody2D bulletInstance;
                bulletInstance = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody2D;
                bulletInstance.AddForce(-barrelEnd.up * launchForce);
            }
           
            shootTimer = 0;
            ammoCurrent--;
            if(ammoCurrent <=0 && magazineCount <= 0) //when the player fully exhausts all bullets and weapon magazines, switch to default weapon
            {
                UpdateWeapon(defaultWeapon);
            }
        }
        
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

    public void StartReloading()
    {
        reloading = true;
        reloadTimer = reloadTimerMax;
        reloadingText.SetActive(true);
    }
    public void ReloadComplete()
    {
        magazineCount--;
        ammoCurrent = ammoMax;
        reloading = false;
        reloadingText.SetActive(false);

    }
}
