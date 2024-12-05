using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigInfo : MonoBehaviour
{
    private PlayerConfig playerConfig;
    public PlayerConfig playerConfigPublic;
    public bool placed = false;
    // Start is called before the first frame update
    void Start()
    {
        placed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPlayerConfig(PlayerConfig info)
    {
        playerConfig = info;
        playerConfigPublic = playerConfig;
    }
}
