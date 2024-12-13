using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject golfballPrefab;
    public GameObject weaponPrefab;
    public GameObject hazardPrefab;

    public float levelXMin;
    public float levelXMax;

    public float levelYMin;
    public float levelYMax;

    private bool needAGolfSpawn = false;
    private bool needAWeaponSpawn = false;
    private bool needAHazardSpawn = false;

    //Timers until next spawncheck occurs
    private float golfBallTimer = 0;
    private float weaponTimer = 0;
    private float hazardTimer = 0;

    public float SpawnTimeForGolfBalls;
    public float SpawnTimeForWeapons;
    public float minSpawnTimeForHazards;
    public float maxSpawnTimeForHazards;
    [SerializeField] private float currentSpawnTimeForHazards;

    //Maximum amount of weapon powerups / golfballs / hazards that can be on screen at once
    public float golfBallLimit = 3;
    public float weaponLimit = 4;
    public int hazardSpawnLimit = 3;
    public int meteorsToSpawn;

    private void Start()
    {
        DetermineHazardTimer();
    }
    void Update()
    {
        golfBallTimer += Time.deltaTime;
        weaponTimer += Time.deltaTime;
        hazardTimer += Time.deltaTime;
        //when the timer elapses and there aren't more than golfBallLimit balls on screen, spawn a golfball
        if (golfBallTimer > SpawnTimeForGolfBalls && GolfBallLimitCheck()) needAGolfSpawn = true;

        if (needAGolfSpawn == true) StartCoroutine(SpawnBall());

        //when the timer elapses and there aren't more than weaponLimit weapons on screen, spawn a weapon
        if (weaponTimer > SpawnTimeForWeapons && WeaponLimitCheck()) needAWeaponSpawn = true;

        if (needAWeaponSpawn == true) StartCoroutine(SpawnWeapon());

        if (hazardPrefab == null) { return; } //if there is no hazards for the map, do not check for hazard related actions

        if (hazardTimer > currentSpawnTimeForHazards) needAHazardSpawn = true;

        if (needAHazardSpawn == true) StartCoroutine(SpawnHazard());

    }

    IEnumerator SpawnBall()
    {
        if (spawnPosIsLegal(0.5f) == true)
        {
            golfBallTimer = 0;
            needAGolfSpawn = false;
            Instantiate(golfballPrefab, transform.position, transform.rotation);
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
            Instantiate(weaponPrefab, transform.position, transform.rotation);
            StopCoroutine(SpawnWeapon());
        }

        yield return null;
    }

    IEnumerator SpawnHazard()
    {
        int succesfullySpawned = 0;
        while (succesfullySpawned < meteorsToSpawn) //try to spawn as hazards until succesfully spawning the needed amount
        {
            if (spawnPosIsLegal(0.5f) == true)
            {
                Debug.Log("successful meteor spawn");
                Instantiate(hazardPrefab, transform.position, transform.rotation);
                succesfullySpawned++;
            }
            else
            {
                Debug.Log("Meteor Spawn Failed");
            }
        }


        hazardTimer = 0;
        needAHazardSpawn = false;
        DetermineHazardTimer();
        StopCoroutine(SpawnWeapon());
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

    //returns true if there are not at maximum amount of golf balls on screen
    public bool GolfBallLimitCheck()
    {
        int amountFound;
        amountFound = FindObjectsOfType<scr_golfBall>().Length;
        Debug.Log("found " + amountFound + " golf balls");
        golfBallTimer = 0;
        return amountFound < golfBallLimit;
    }

    //returns true if there are not at maximum amount of power ups on screen
    public bool WeaponLimitCheck()
    {
        int amountFound;
        amountFound = FindObjectsOfType<WeaponPickup>().Length;
        Debug.Log("found " + amountFound + " weapons");
        weaponTimer = 0;
        return amountFound < weaponLimit;
    }

    //randomly choose the timer until the next hazard spawn between the minimum and maximum spawn time
    public void DetermineHazardTimer()
    {
        if (hazardPrefab == null) { return; }
        currentSpawnTimeForHazards = Random.Range(minSpawnTimeForHazards, maxSpawnTimeForHazards);
        meteorsToSpawn = Random.Range(1, hazardSpawnLimit);
    }
}
