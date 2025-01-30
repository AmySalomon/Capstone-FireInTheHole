using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpecialEvent/SpawnHazard")]

public class SpawnHazard : SpecialEvents
{
    public int hazardSpawnMaximum;
    public int hazardSpawnMinimum;
    private int hazardSpawnAmount;
  
    public override void InitiateEvent()
    {
        hazardSpawnAmount = Random.Range(hazardSpawnMinimum, hazardSpawnMaximum+1);

        int succesfullySpawned = 0;
        while (succesfullySpawned < hazardSpawnAmount) //try to spawn as hazards until succesfully spawning the needed amount
        {
            if (spawnManager.checkSpawnPosIsLegal(0.5f) == true)
            {
                Debug.Log("successful hazard spawn");
                Instantiate(hazardPrefab, spawnManager.transform.position, spawnManager.transform.rotation);
                succesfullySpawned++;
            }
            else
            {
                Debug.Log("Hazard Spawn Failed");
            }
        }

    }

    
}
