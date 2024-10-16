using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject playerStuff;
    public GameObject playerCanvasStuff;

    public float levelXMin;
    public float levelXMax;

    public float levelYMin;
    public float levelYMax;

    public float respawnTime;

    [HideInInspector] public bool playerIsDead = false;
    private float timer = 0;
    private AudioSource killSound;
    private SpriteRenderer deathIcon;
    private Dash dash;

    private Vector2 moveToPosition;

    private void Awake()
    {
        killSound = GetComponent<AudioSource>();
        deathIcon = GetComponent<SpriteRenderer>();
        dash = GetComponentInChildren<Dash>();
        deathIcon.enabled = false;
    }
    void Update()
    {
        if (playerIsDead)
        {
            transform.position = moveToPosition;
            timer += Time.deltaTime;

            if (timer > 1) deathIcon.enabled = false;
            else if (timer > 0.8) deathIcon.enabled = true;
            else if (timer > 0.6) deathIcon.enabled = false;
            else if (timer > 0.4) deathIcon.enabled = true;
            else if (timer > 0.2) deathIcon.enabled = false;
            else if (timer > 0) deathIcon.enabled = true;


            if (timer > respawnTime) StartCoroutine(SpawnPlayer());
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
        if (spawnPosIsLegal() == true)
        {
            deathIcon.enabled = false;
            playerIsDead = false;
            playerStuff.transform.localPosition = Vector2.zero;
            playerStuff.SetActive(true);
            playerCanvasStuff.SetActive(true);
            timer = 0;
            dash.dashRechargeTimer = dash.dashRechargeAmount;
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
            return true;
        }
        else
        {
            Debug.Log(collider.gameObject.name);
            return false;
        }
    }
}
