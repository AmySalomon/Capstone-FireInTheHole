using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SpecialEvent/Event")]
[System.Serializable]
public class SpecialEvent : ScriptableObject
{
    public bool variableSpawnTime;
    public bool spawnObjects;

    public float minSpawnTimeForEvent;//minimum amount of time to pass until an event occurs
    public float maxSpawnTimeForEvent;//maximum amount of time to pass until an event occurs
    public float spawnTimeForEvent; //fixed amount of time to pass until an event occurs
    public GameObject spawnedObject; //whatever objects this event can Spawn

}


[CustomEditor(typeof(SpecialEvent))]
public class SpecialEventEditor : Editor
{
     public override void OnInspectorGUI()
    {
        SpecialEvent specialEvent = target as SpecialEvent;


        specialEvent.variableSpawnTime = EditorGUILayout.Toggle("Variable Spawn Time?", specialEvent.variableSpawnTime );
        specialEvent.spawnObjects = EditorGUILayout.Toggle("Spawn Objects?", specialEvent.spawnObjects);

        if (specialEvent.variableSpawnTime)
        {
            specialEvent.minSpawnTimeForEvent = EditorGUILayout.FloatField("Min Event Spawn Time", specialEvent.minSpawnTimeForEvent);
            specialEvent.maxSpawnTimeForEvent = EditorGUILayout.FloatField("Max Event Spawn Time", specialEvent.maxSpawnTimeForEvent);
        }
        else
        {
            specialEvent.spawnTimeForEvent = EditorGUILayout.FloatField("Event Spawn Time", specialEvent.spawnTimeForEvent);
            if(specialEvent.minSpawnTimeForEvent == 0)
            {
                specialEvent.minSpawnTimeForEvent = specialEvent.maxSpawnTimeForEvent;
            }
            if(specialEvent.maxSpawnTimeForEvent == 0)
            {
                specialEvent.maxSpawnTimeForEvent = specialEvent.minSpawnTimeForEvent;
            }
        }

        if (specialEvent.spawnObjects)
        {
            specialEvent.spawnedObject = (GameObject)EditorGUILayout.ObjectField("Game Object To Spawn", specialEvent.spawnedObject, typeof(GameObject), true);
        }

    }
}
