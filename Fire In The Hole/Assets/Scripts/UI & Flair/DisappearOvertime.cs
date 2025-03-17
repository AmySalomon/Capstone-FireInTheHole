using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOvertime : MonoBehaviour
{
    private float timer;

    public float timeToDelete = 2;


    public bool shouldIDelete = true;



    // Update is called once per frame
    void Update()
    {
        if (shouldIDelete) timer += Time.deltaTime;

        if (timer > timeToDelete)
        {
            Destroy(gameObject);
        }
    }
}
