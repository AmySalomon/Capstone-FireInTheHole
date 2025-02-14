using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
public class ShootProjectile : MonoBehaviour
{
    //gets bullet prefab
    public Rigidbody2D bullet;
    //gets barrel position
    public Transform barrelEnd;
    //muzzle flash sprite
    public GameObject muzzleFlash;
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
    public ShotType shotType; //how the gun shoots (multishot is only one other one than basic for now)
    public float shotSpread; //the angle at which a bullet can be modified when shooting
    public bool hasLaser; //whether or not the gun has a laserpointer (sniper only)
    private GameObject myCamera;

    public WeaponClass defaultWeapon, currentWeapon;
    public SpriteRenderer currentGunSprite;
    public bool reloading = false;

    [HideInInspector] public bool isTryingToShoot;
    private PlayerInputHandler rumbleHandler;

    [SerializeField] private GameObject reloadingText;
    [SerializeField] private TextMeshProUGUI magazineText;

    public GameObject bullet_UI;
    public Sprite usedBullet_UI;
    public Transform bulletPanelParent;
    public List<GameObject> ammo_UI = new List<GameObject>();

    private float currentCameraX;
    private float currentCameraY;
    private float currentCameraZ;
    private void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioPlayer = GetComponent<AudioSource>();
        reloadingText.SetActive(false);
        rumbleHandler = GetComponentInParent<PlayerInputHandler>();
        UpdateWeapon(defaultWeapon); //Set starting weapon to player default weapon

        //gets the current camera's position for screen shake later
        currentCameraX = myCamera.transform.position.x;
        currentCameraY = myCamera.transform.position.y;
        currentCameraZ = myCamera.transform.position.z;
    }
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (reloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                ReloadComplete();
            }
        }
        //Screenshake and muzzle flash
        if (shootTimer <= 0.1f)
        {
            myCamera.transform.position = new Vector3(currentCameraX + Random.Range(0, screenShake), currentCameraY + Random.Range(0, screenShake), currentCameraZ + Random.Range(0, screenShake));
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
            Debug.Log("shoot");
            muzzleFlash.GetComponent<MuzzleAnimation>().StartAnimation();
            audioPlayer.pitch = Random.Range(0.9f, 1.1f);
            audioPlayer.PlayOneShot(gunshot, 1f);
            rumbleHandler.StartRumble();
            shotType.ShootBullets(barrelEnd, launchForce, shotSpread, this.gameObject);
            shootTimer = 0;
            Destroy(ammo_UI[ammoCurrent - 1]);
            ammo_UI.RemoveAt((int)ammoCurrent-1);
            ammoCurrent--;
            AutoReloadCheck();
        }
        
    }

    public void AutoReloadCheck() // when the player is out of bullets, check if we need to switch to the default weapon otherwise try to reload
    {
        if (ammoCurrent <= 0) 
        {
            if (magazineCount <= 0)
            {
                UpdateWeapon(defaultWeapon);
            }
            else
            {
                StartReloading();
            }
        }
    }

    public void ManualReloadCheck() //when the player tries to reload, check if we need to switch to the default weapon
    {
        if(reloading) { return; }
        if(magazineCount <= 0)
        {
            UpdateWeapon(defaultWeapon);
        }
        else { StartReloading(); }
    }

    public void UpdateWeapon(WeaponClass newWeapon) //Called on picking up a new Weapon
    {
        //update bullet UI for new bullets
        for (int i = 0; i < ammo_UI.Count; i++)
        {
            Destroy(ammo_UI[i]);
        }
        ammo_UI.Clear();
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
        shotType = newWeapon.behaviour;
        shotSpread = newWeapon.shotSpread;
        hasLaser = newWeapon.hasLaser;

        for (int i = 0; i < ammoMax; i++)
        {
            UpdateBulletUI(newWeapon);
        }
        //hide magazine count if the weapon is the default weapon
        if(newWeapon.gunType == defaultWeapon.gunType)
        {
            magazineText.text = null;
        }
        else
        {
            magazineText.text = magazineCount.ToString();
        }
    }

    public void UpdateShotType(ShotType newShotType)
    {
        shotType = newShotType;
    }

    public void StartReloading()
    {
        reloading = true;
        reloadTimer = reloadTimerMax;
        reloadingText.SetActive(true);
    }
    public void ReloadComplete()
    {
        for (int i = 0; i < ammo_UI.Count; i++)
        {
            Destroy(ammo_UI[i]);
        }
        ammo_UI.Clear();
        magazineCount--;
        ammoCurrent = ammoMax;
        reloading = false;
        reloadingText.SetActive(false);
        for (int i = 0; i < ammoMax; i++)
        {
            UpdateBulletUI(currentWeapon);
        }
        Debug.Log("current weapon is " +currentWeapon.gunType+" and default weapon is "+defaultWeapon.gunType);
        if (currentWeapon.gunType == defaultWeapon.gunType)
        {
            magazineText.text = "";
            Debug.Log("Magazine text: " + magazineText.text);
        }
        else
        {
            magazineText.text = magazineCount.ToString();
        }
    }

    public void UpdateBulletUI(WeaponClass newWeapon)
    {
        GameObject bulletUI = Instantiate(bullet_UI, bulletPanelParent);
        bulletUI.GetComponent<Image>().sprite = newWeapon.bulletUI;
        ammo_UI.Add(bulletUI);
    }
}
