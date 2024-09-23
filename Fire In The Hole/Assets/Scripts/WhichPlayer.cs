using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhichPlayer : MonoBehaviour
{
    public int thisPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<PlayerInput>().playerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
