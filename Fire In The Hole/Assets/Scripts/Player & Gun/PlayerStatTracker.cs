using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script responsible with updating the player stats variable in player config with a player's stats
public class PlayerStatTracker : MonoBehaviour
{
    public PlayerConfig myConfig;

    //violence methods
    public void UpdateDeaths()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.deaths++;
        Debug.Log("Updating Deaths: value is now " + myConfig.Stats.deaths);
    }

    public void UpdateKills()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.kills++;
        Debug.Log("Updating Kills: value is now " + myConfig.Stats.kills);
    }

    public void UpdateShotsFired()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.shotsFired++;
        Debug.Log("Updating shots fired: value is now " + myConfig.Stats.shotsFired);

    }

    public void UpdateGolfballKills()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.golfballKills++;
        Debug.Log("Updating golfball Kills: value is now " + myConfig.Stats.golfballKills);

    }

    public void UpdateSelfDestructs()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.selfDestructs++;
        Debug.Log("Updating SDs: value is now " + myConfig.Stats.selfDestructs);

    }

    public void UpdateDeathsBy(GameObject killer)
    {
        if (myConfig == null) { return; }
        if (myConfig.Stats.deathsBy.TryGetValue(killer, out float value))
        {
            value++;
            myConfig.Stats.deathsBy[killer] = value;
        }
        else
        {
            myConfig.Stats.deathsBy.Add(killer, 1);
        }
        Debug.Log("Updating deathsBy: "+killer+" has killed me this many times: " + myConfig.Stats.deathsBy[killer]);

    }

    //putting methods
    public void UpdatePuttsTaken()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.puttsTaken++;
        Debug.Log("Updating putts taken: value is now " + myConfig.Stats.puttsTaken);

    }

    public void UpdatePuttsMissed()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.puttsMissed++;
        Debug.Log("Updating putts missed: value is now " + myConfig.Stats.puttsMissed);

    }

    //interactables methods

    public void UpdatePowerUpsGained()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.powerupsGained++;
        Debug.Log("Updating powerups gained: value is now " + myConfig.Stats.powerupsGained);

    }

    public void UpdateWeaponsGained()
    {
        if (myConfig == null) { return; }
        myConfig.Stats.weaponsGained++;
        Debug.Log("Updating weapons gained: value is now " + myConfig.Stats.weaponsGained);

    }
}
