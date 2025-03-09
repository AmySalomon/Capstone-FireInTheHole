using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject playerStuff;
    public GameObject playerCanvasStuff;
    public scr_powerUpManager playerPowerUpManager;

    public GameObject spawnIndicatorPrefab;
    public GameObject deathAnimationPrefab;

    [HideInInspector] public Vector3 deathDirection;

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

    [HideInInspector] public int myIndex;

    [HideInInspector] public bool tryingToAttack = false;

    //gets the current ring indicator, in order to move it for the helldivers respawn
    private GameObject currentRingIndicator;

    private void Awake()
    {
        killSound = GetComponent<AudioSource>();
        dash = GetComponentInChildren<Dash>();
        gun = GetComponentInChildren<ShootProjectile>();
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
            if (setSpawnLocation != null)
            {
                //spawns player instantly if they die in the tutorial
                respawnPosition = setSpawnLocation.position;
                spawnRings = false;
                StartCoroutine(SpawnPlayer());
            }
            else
            {
                transform.position = moveToPosition;
                timer += Time.deltaTime;


                if (spawnRings && timer > respawnIndicatorTime) SpawnRingIndicators();
                if (timer > respawnTime) StartCoroutine(SpawnPlayer());
            }
        }

        if (playerJustRespawned && setSpawnLocation == null)
        {
            invulnTimer += Time.deltaTime;
            //this is to turn off invulnerability early IF the player is trying to attack others off of spawn
            if (tryingToAttack == true) invulnTimer += invulnRespawnTime * 0.8f;
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
        tryingToAttack = false;
    }

    public void Died()
    {
        moveToPosition = playerStuff.transform.position;
        playerIsDead = true;
        killSound.Play();
        if (setSpawnLocation == false)
        {
            var deadPlayer = Instantiate(deathAnimationPrefab, moveToPosition, Quaternion.identity);
            deadPlayer.GetComponent<DeathAnimation>().receivedSprite = mySprite;
            deadPlayer.GetComponent<DeathAnimation>().knockbackDirection = deathDirection;

        }
        playerStuff.SetActive(false);
        playerCanvasStuff.SetActive(false);
        playerPowerUpManager.RemovePowerUp();
    }

    IEnumerator SpawnPlayer()
    {
        if (spawnRings == false)
        {
            transform.position = respawnPosition;
            playerIsDead = false;
            //resets whether the rings should spawn or not (should only happen once per respawn)
            spawnRings = true;
            playerStuff.transform.localPosition = Vector2.zero;
            playerStuff.SetActive(true);
            playerCanvasStuff.SetActive(true);
            timer = 0;
            //make player invulnerable, when not on tutorial
            if (setSpawnLocation == false) playerRigidbody.excludeLayers = bullet + golfBall;
            //sets dashes to full
            dash.dashRechargeTimer = dash.dashRechargeAmount;
            //give player default gun
            gun.UpdateWeapon(gun.defaultWeapon);
            playerJustRespawned = true;
            playerPowerUpManager.RemovePowerUp();
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
