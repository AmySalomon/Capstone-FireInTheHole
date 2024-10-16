using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject golfballPrefab;
    public GameObject weaponPrefab;

    public float levelXMin;
    public float levelXMax;

    public float levelYMin;
    public float levelYMax;

    private bool needAGolfSpawn = false;
    private bool needAWeaponSpawn = false;

    private float golfBallTimer = 0;
    private float weaponTimer = 0;

    public float SpawnTimeForGolfBalls;
    public float SpawnTimeForWeapons;
    void Update()
    {
        golfBallTimer += Time.deltaTime;
        weaponTimer += Time.deltaTime;

        if (golfBallTimer > SpawnTimeForGolfBalls) needAGolfSpawn = true;

        if (needAGolfSpawn == true) StartCoroutine(SpawnBall());

        if (weaponTimer > SpawnTimeForWeapons) needAWeaponSpawn = true;

        if (needAWeaponSpawn == true) StartCoroutine(SpawnWeapon());

    }

    IEnumerator SpawnBall()
    {
        if (spawnPosIsLegal() == true)
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
        if (spawnPosIsLegal() == true)
        {
            weaponTimer = 0;
            needAWeaponSpawn = false;
            Instantiate(weaponPrefab, transform.position, transform.rotation);
            StopCoroutine(SpawnWeapon());
        }

        yield return null;
    }

    private bool spawnPosIsLegal(float radius = .5f)
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
}
