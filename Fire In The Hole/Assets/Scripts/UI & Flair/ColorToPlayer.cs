using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorToPlayer : MonoBehaviour
{

    [HideInInspector] public Color myColor;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = myColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
