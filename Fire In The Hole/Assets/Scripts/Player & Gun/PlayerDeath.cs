using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject playerStuff;
    public GameObject playerCanvasStuff;

    public GameObject spawnIndicatorPrefab;

    [HideInInspector] public Color myColor;
    [HideInInspector] public Sprite mySprite;

    public float levelXMin;
    public float levelXMax;

    public float levelYMin;
    public float levelYMax;

    //indicator determines when the spawn indicator ring should appear ; this should happen (insert ring closing time) seconds before the respawn time 
    //(ex: rings close in 2 seconds, therefore, if respawn time is 3 seconds, rings should appear 1 second into respawn time)
    public float respawnTime;
    public float respawnIndicatorTime;
    private bool spawnRings = true;

    public float invulnRespawnTime;

    [HideInInspector] public bool playerIsDead = false;
    private bool playerJustRespawned = false;

    //player sprite so we can make it flash on respawn
    [SerializeField] private SpriteRenderer playerSprite;
    private float timer = 0;
    private float invulnTimer = 0;
    private AudioSource killSound;
    private SpriteRenderer deathIcon;
    private Dash dash;
    private ShootProjectile gun;
    public Rigidbody2D playerRigidbody;

    private Vector2 moveToPosition;

    //layermasks we want to exclude for player respawn invulnerability
    public LayerMask bullet;
    public LayerMask golfBall;
    public LayerMask none;

    private Vector3 respawnPosition;

    [HideInInspector] public Transform setSpawnLocation;

    //gets the current ring indicator, in order to move it for the helldivers respawn
    private GameObject currentRingIndicator;

    private void Awake()
    {
        killSound = GetComponent<AudioSource>();
        deathIcon = GetComponent<SpriteRenderer>();
        dash = GetComponentInChildren<Dash>();
        gun = GetComponentInChildren<ShootProjectile>();
        deathIcon.enabled = false;
    }
    void Update()
    {
        if (currentRingIndicator != null)
        {
            respawnPosition = currentRingIndicator.transform.position;
        }
        //kills player, flashes kill indicator, waits to respawn player
        if (playerIsDead)
        {
            transform.position = moveToPosition;
            timer += Time.deltaTime;

            //flash kill indicator
            if (timer > 1) deathIcon.enabled = false;
            else if (timer > 0.8) deathIcon.enabled = true;
            else if (timer > 0.6) deathIcon.enabled = false;
            else if (timer > 0.4) deathIcon.enabled = true;
            else if (timer > 0.2) deathIcon.enabled = false;
            else if (timer > 0) deathIcon.enabled = true;

            if (spawnRings && timer > respawnIndicatorTime) SpawnRingIndicators();
            if (timer > respawnTime) StartCoroutine(SpawnPlayer());
        }

        if (playerJustRespawned)
        {
            invulnTimer += Time.deltaTime;
            //make player invulnerable
            playerRigidbody.excludeLayers = bullet + golfBall;
            //flash player sprite when invulnerable, when time runs out, re-enable player damage via layers
            if (invulnTimer > invulnRespawnTime)
            {
                playerSprite.enabled = true;
                playerRigidbody.excludeLayers = none;
                invulnTimer = 0;
                playerJustRespawned = false;
            }
            else if (invulnTimer > invulnRespawnTime / 10 * 10) playerSprite.enabled = false;
            else if (invulnTimer > invulnRespawnTime / 10 * 9) playerSprite.enabled = true;
            else if (invulnTimer > invulnRespawnTime / 10 * 8) playerSprite.enabled = false;
            else if (invulnTimer > invulnRespawnTime / 10 * 7) playerSprite.enabled = true;
            else if (invulnTimer > invulnRespawnTime / 10 * 6) playerSprite.enabled = false;
            else if (invulnTimer > invulnRespawnTime / 10 * 5) playerSprite.enabled = true;
            else if (invulnTimer > invulnRespawnTime / 10 * 4) playerSprite.enabled = false;
            else if (invulnTimer > invulnRespawnTime / 10 * 3) playerSprite.enabled = true;
            else if (invulnTimer > invulnRespawnTime / 10 * 2) playerSprite.enabled = false;
            else if (invulnTimer > 0) playerSprite.enabled = true;
        }
        
    }

    public void Died()
    {
        moveToPosition = playerStuff.transform.position;
        playerIsDead = true;
        killSound.Play();
        playerStuff.SetActive(false);
        playerCanvasStuff.SetActive(false);
    }

    IEnumerator SpawnPlayer()
    {
        if (spawnRings == false)
        {
            transform.position = respawnPosition;
            deathIcon.enabled = false;
            playerIsDead = false;
            //resets whether the rings should spawn or not (should only happen once per respawn)
            spawnRings = true;
            playerStuff.transform.localPosition = Vector2.zero;
            playerStuff.SetActive(true);
            playerCanvasStuff.SetActive(true);
            timer = 0;
            //make player invulnerable
            playerRigidbody.excludeLayers = bullet + golfBall;
            //sets dashes to full
            dash.dashRechargeTimer = dash.dashRechargeAmount;
            //give player default gun
            gun.UpdateWeapon(gun.defaultWeapon);
            playerJustRespawned = true;
            StopCoroutine(SpawnPlayer());
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
            respawnPosition = transform.position;
            return true;
        }
        else
        {
            //Debug.Log(collider.gameObject.name);
            return false;
        }
    }

    void SpawnRingIndicators()
    {
        if (setSpawnLocation != null)
        {
            transform.position = setSpawnLocation.position;
            respawnPosition = transform.position;
            currentRingIndicator = GameObject.Instantiate(spawnIndicatorPrefab, transform.position, Quaternion.identity) as GameObject;
            currentRingIndicator.GetComponent<SpawnRing>().spawnPlayer = true;
            currentRingIndicator.GetComponent<SpawnRing>().myColor = myColor;
            currentRingIndicator.GetComponent<SpawnRing>().mySprite = mySprite;
            spawnRings = false;
        }
        else if (spawnPosIsLegal() == true)
        {
            currentRingIndicator = GameObject.Instantiate(spawnIndicatorPrefab, transform.position, Quaternion.identity) as GameObject;
            currentRingIndicator.GetComponent<SpawnRing>().spawnPlayer = true;
            currentRingIndicator.GetComponent<SpawnRing>().myColor = myColor;
            currentRingIndicator.GetComponent<SpawnRing>().mySprite = mySprite;
            spawnRings = false;
        }
    }

    public void MoveRespawnIndicator(Vector2 vector)
    {
       if (currentRingIndicator != null)
        {
            currentRingIndicator.GetComponent<SpawnRing>().respawnMovement = vector;
        }
       
    }
}
