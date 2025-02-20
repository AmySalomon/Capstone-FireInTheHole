using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //with new spawn system, the following prefabs are no longer necessary
    //public GameObject golfballPrefab;
    //public GameObject weaponPrefab;
    public GameObject hazardPrefab;

    public SpecialEvents specialEvents;


    public GameObject ringSpawnerPrefab;


    public float levelXMin;
    public float levelXMax;

    public float levelYMin;
    public float levelYMax;

    private bool needAGolfSpawn = false;
    private bool needAWeaponSpawn = false;
    private bool needAHazardSpawn = false;
    public bool needAPowerUpSpawn = false;

    //Timers until next spawncheck occurs
    private float golfBallTimer = 0;
    private float weaponTimer = 0;
    private float hazardTimer = 0;
    public float powerUpTimer = 0;

    public float SpawnTimeForGolfBalls;
    public float SpawnTimeForWeapons;
    public float SpawnTimeForPowerUps;
    public float minSpawnTimeForHazards;
    public float maxSpawnTimeForHazards;
    [SerializeField] private float currentSpawnTimeForHazards;

    //Maximum amount of weapon powerups / golfballs that can be on screen at once
    public float golfBallLimit = 3;
    public float weaponLimit = 4;
    public float PowerupLimit = 5; //change as needed
    

    private void Start()
    {
        InitiateEventSetup();
        DetermineHazardTimer();
    }

    private void InitiateEventSetup()
    {
        if (specialEvents == null) { return; } //if there is no events for the map, do not check for hazard related actions
        specialEvents.EventSetup(this.gameObject);
        minSpawnTimeForHazards = specialEvents.minSpawnTimeForEvent;
        maxSpawnTimeForHazards = specialEvents.maxSpawnTimeForEvent;
    }

    void Update()
    {
        golfBallTimer += Time.deltaTime;
        weaponTimer += Time.deltaTime;
        hazardTimer += Time.deltaTime;
        powerUpTimer += Time.deltaTime;

        //when the timer elapses and there aren't more than golfBallLimit balls on screen, spawn a golfball
        if (golfBallTimer > SpawnTimeForGolfBalls && GolfBallLimitCheck()) needAGolfSpawn = true;

        if (needAGolfSpawn == true) StartCoroutine(SpawnBall());

        //when the timer elapses and there aren't more than weaponLimit weapons on screen, spawn a weapon
        if (weaponTimer > SpawnTimeForWeapons && WeaponLimitCheck()) needAWeaponSpawn = true;

        if (needAWeaponSpawn == true) StartCoroutine(SpawnWeapon());

        if (powerUpTimer > SpawnTimeForPowerUps && PowerUpLimitCheck()) needAPowerUpSpawn = true;
        
        if (needAPowerUpSpawn == true) StartCoroutine(SpawnPowerup());

        if (specialEvents == null) { return; } //if there is no special events for the map, do not check for hazard related actions

        if (hazardTimer > currentSpawnTimeForHazards) needAHazardSpawn = true;

        if (needAHazardSpawn == true) StartCoroutine(SpawnHazard());



    }

    IEnumerator SpawnBall()
    {
        if (spawnPosIsLegal(0.5f) == true)
        {
            golfBallTimer = 0;
            needAGolfSpawn = false;
            GameObject newBall = GameObject.Instantiate(ringSpawnerPrefab, transform.position, Quaternion.identity) as GameObject;
            newBall.GetComponent<SpawnRing>().spawnBall = true;
            StopCoroutine(SpawnBall());
        }

        yield return null;
    }

    IEnumerator SpawnWeapon()
    {
        if (spawnPosIsLegal(0.5f) == true)
        {
            weaponTimer = 0;
            needAWeaponSpawn = false;
            GameObject newGun = GameObject.Instantiate(ringSpawnerPrefab, transform.position, Quaternion.identity) as GameObject;
            newGun.GetComponent<SpawnRing>().spawnGun = true;
            StopCoroutine(SpawnWeapon());
        }

        yield return null;
    }

    IEnumerator SpawnPowerup()
    {
        Debug.Log("trying to spawn PowerUp");
        if (spawnPosIsLegal(0.5f) == true)
        {
            
            powerUpTimer = 0;
            needAPowerUpSpawn = false;
            GameObject newPowerUp = GameObject.Instantiate(ringSpawnerPrefab, transform.position, Quaternion.identity) as GameObject;
            newPowerUp.GetComponent<SpawnRing>().spawnPowerup = true;
            //newPowerUp.GetComponent <SpawnRing>().SpawnInThing();
            StopCoroutine(SpawnPowerup());
        }
            yield return null;
    }

    IEnumerator SpawnHazard()
    {
        specialEvents.InitiateEvent();
        //int succesfullySpawned = 0;
        //while (succesfullySpawned < meteorsToSpawn) //try to spawn as hazards until succesfully spawning the needed amount
        //{
        //    if (spawnPosIsLegal(0.5f) == true)
        //    {
        //        Debug.Log("successful meteor spawn");
        //        Instantiate(hazardPrefab, transform.position, transform.rotation);
        //        succesfullySpawned++;
        //    }
        //    else
        //    {
        //        Debug.Log("Meteor Spawn Failed");
        //    }
        //}


        hazardTimer = 0;
        needAHazardSpawn = false;
        DetermineHazardTimer();
        StopCoroutine(SpawnHazard());
        yield return null;
    }

    private bool spawnPosIsLegal(float radius)
    {
        Vector2 randomSpawnPosition = new Vector2(Random.Range(levelXMin, levelXMax), Random.Range(levelYMin, levelYMax));

        transform.position = randomSpawnPosition;

        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius);

        if (collider == null)
        {
            return true;
        }
        else
        {
            Debug.Log(collider.gameObject.name);
            return false;
        }
    }

    //allows external objects to run the spawn position checker
    public bool checkSpawnPosIsLegal(float radius)
    {
        return spawnPosIsLegal(radius);
    }

    //returns true if there are not at maximum amount of golf balls on screen
    public bool GolfBallLimitCheck()
    {
        int amountFound;
        amountFound = FindObjectsOfType<scr_golfBall>().Length;
        Debug.Log("found " + amountFound + " golf balls");
        golfBallTimer = 0;
        return amountFound < golfBallLimit;
    }

    //returns true if there are not at maximum amount of weapons on screen
    public bool WeaponLimitCheck()
    {
        int amountFound;
        amountFound = FindObjectsOfType<WeaponPickup>().Length;
        Debug.Log("found " + amountFound + " weapons");
        weaponTimer = 0;
        return amountFound < weaponLimit;
    }

    //returns true if there are not at maximum amount of power ups on screen
    public bool PowerUpLimitCheck()
    {
        int amountFound;
        amountFound = FindObjectsOfType<scr_powerUpPickup>().Length;
        Debug.Log("found " + amountFound + " power ups");
        powerUpTimer = 0;
        return amountFound < PowerupLimit;
    }

    //randomly choose the timer until the next hazard spawn between the minimum and maximum spawn time
    public void DetermineHazardTimer()
    {
        if (specialEvents == null) { return; }
        currentSpawnTimeForHazards = Random.Range(minSpawnTimeForHazards, maxSpawnTimeForHazards);
    }
}
