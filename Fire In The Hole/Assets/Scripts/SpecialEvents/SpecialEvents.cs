using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpecialEvent")]

public abstract class SpecialEvents : ScriptableObject
{
    public string eventName;
    public GameObject hazardPrefab;
    public float minSpawnTimeForEvent;
    public float maxSpawnTimeForEvent;
    [HideInInspector]
    public SpawnManager spawnManager;

    public abstract void InitiateEvent();

    public virtual void EventSetup(GameObject theManager)
    {
        spawnManager = theManager.GetComponent<SpawnManager>();
    }
}
