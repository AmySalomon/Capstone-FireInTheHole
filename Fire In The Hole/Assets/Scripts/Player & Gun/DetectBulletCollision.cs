using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBulletCollision : MonoBehaviour
{
    public PlayerDeath deathManager;
    [SerializeField] public int killValueRef = 1; //how many points a player's life is worth
    private int killValue = 1;
    public GameObject playerKiller;

    private void Start()
    {
        killValue = killValueRef;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //gets the vector direction from which the player was killed to use for the death animation
            if (collision != null) deathManager.deathDirection = collision.transform.position - transform.position;
            //if a player was killed by someone, update their stats
            playerKiller = IdentifyKillerCollision(collision);
            GiveKillCredit(playerKiller);
            deathManager.Died();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //gets the vector direction from which the player was killed to use for the death animation
            if (collision != null) deathManager.deathDirection = collision.transform.position - transform.position;
            //if a player was killed by someone, update their stats
            playerKiller = IdentifyKillerCollider(collision);
            GiveKillCredit(playerKiller);
            deathManager.Died();
        }
    }

    public void GiveKillCredit(GameObject playerShooter) //assign the player who killed the gameobject kill credit in their PlayerStats
    {
        if(playerShooter == null ) return;
        if(playerShooter == this.gameObject) //if you killed yourself, +1 to self destructs
        {
            this.gameObject.GetComponent<PlayerStatTracker>().UpdateSelfDestructs();
        }
        else //if someone else killed you, +1 to dead by, and +1 to their kills
        {
            this.gameObject.GetComponent<PlayerStatTracker>().UpdateDeathsBy(playerShooter);
            playerShooter.GetComponent<PlayerStatTracker>().UpdateKills();
        }
    }

    public GameObject IdentifyKillerCollider(Collider2D collision)
    {
        GameObject killer = null;
        if (collision.gameObject.TryGetComponent<BulletManager>(out BulletManager bulletManager)) //if they died via gun
        {
            killer = bulletManager.playerShooter;
        }
        if (collision.gameObject.TryGetComponent<Explosion>(out Explosion explosion)) //if they died via rocket launcher explosion
        {
            killer = explosion.playerShooter;
        }
        if(collision.gameObject.TryGetComponent<scr_golfBall>(out scr_golfBall ball)) //if they died via golf ball
        {
            killer = ball.playerHitter.GetComponentInChildren<PlayerScore>().gameObject;

            //give the player who killed someone with a golfball credit... unless it was themselves
            if(killer != this.gameObject)
            {
                killer.GetComponent<PlayerStatTracker>().UpdateGolfballKills();
            }
        }
        if (collision.gameObject.GetComponentInParent<scr_meleeSwing>()) //if they died via melee attack
        {
            killer = collision.gameObject.GetComponentInParent<PlayerScore>().gameObject;
        }
        if(collision.gameObject.TryGetComponent<ExplosionAnimation>(out  ExplosionAnimation explosionAnimation)) //if they died via another player respawning
        {
            killer = explosionAnimation.explosionCreator;
        }
        return killer;
    }

    public GameObject IdentifyKillerCollision(Collision2D collision)
    {
        GameObject killer = null;
        if (collision.gameObject.TryGetComponent<BulletManager>(out BulletManager bulletManager)) //if they died via gun
        {
            killer = bulletManager.playerShooter;
        }
        if (collision.gameObject.TryGetComponent<Explosion>(out Explosion explosion)) //if they died via rocket launcher explosion
        {
            killer = explosion.playerShooter;
        }
        if (collision.gameObject.TryGetComponent<scr_golfBall>(out scr_golfBall ball)) //if they died via golf ball
        {
            killer = ball.playerHitter.GetComponentInChildren<PlayerScore>().gameObject;
            //give the player who killed someone with a golfball credit... unless it was themselves
            if (killer != this.gameObject && killer != null)
            {
                killer.GetComponent<PlayerStatTracker>().UpdateGolfballKills();
            }
        }
        if (collision.gameObject.GetComponentInParent<scr_meleeSwing>()) //if they died via melee attack
        {
            killer = collision.gameObject.GetComponentInParent<PlayerScore>().gameObject;
        }
        if (collision.gameObject.TryGetComponent<ExplosionAnimation>(out ExplosionAnimation explosionAnimation)) //if they died via another player respawning
        {
            killer = explosionAnimation.explosionCreator;
        }
        return killer;
    }
}
