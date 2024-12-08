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
        muzzleFlash.enabled = false;
        reloadingText.SetActive(false);
        UpdateWeapon(defaultWeapon); //Set starting weapon to player default weapon

        //gets the current camera's position for screen shake later
        currentCameraX = myCamera.transform.position.x;
        currentCameraY = myCamera.transform.position.y;
        currentCameraZ = myCamera.transform.position.z;
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
        //Screenshake and muzzle flash
        if (shootTimer <= 0.1f)
        {
            muzzleFlash.enabled = true;
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
            if(ammoCurrent <= 0)
            {
                StartReloading();
                return;
            }
            Debug.Log("shoot");
            audioPlayer.pitch = Random.Range(0.9f, 1.1f);
            audioPlayer.PlayOneShot(gunshot, 1f);
            currentWeapon.behaviour.ShootBullets(barrelEnd, launchForce);
            shootTimer = 0;
            Destroy(ammo_UI[ammoCurrent - 1]);
            ammo_UI.RemoveAt((int)ammoCurrent-1);
            ammoCurrent--;
            if(ammoCurrent <=0 && magazineCount <= 0) //when the player fully exhausts all bullets and weapon magazines, switch to default weapon
            {
                UpdateWeapon(defaultWeapon);
            }
        }
        
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
        if (currentWeapon.gunType == defaultWeapon.gunType)
        {
            magazineText.text = null;
        }
        else
        {
            magazineText.text = magazineCount.ToString();
        }
        magazineText.text = magazineCount.ToString();
    }

    public void UpdateBulletUI(WeaponClass newWeapon)
    {
        GameObject bulletUI = Instantiate(bullet_UI, bulletPanelParent);
        bulletUI.GetComponent<Image>().sprite = newWeapon.bulletUI;
        ammo_UI.Add(bulletUI);
    }
}
