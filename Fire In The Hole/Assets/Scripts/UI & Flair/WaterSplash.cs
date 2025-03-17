using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    private SpriteRenderer mySprite;

    [HideInInspector] public bool amInWater = false;
    void Start()
    {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        mySprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (amInWater) mySprite.enabled = true;
        else mySprite.enabled = false;
    }
}
