using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlasnOnOff : MonoBehaviour
{
    public float maxTime = 1;

    private float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > maxTime / 2)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        if (time > maxTime)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            time = 0;
        }
    }
}
