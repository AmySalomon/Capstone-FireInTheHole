using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigInfo : MonoBehaviour
{
    private PlayerConfig playerConfig;
    public PlayerConfig playerConfigPublic;
    public Transform crosshair, playerSprite;
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
        UpdatePlayerInfo();
    }

    public void UpdatePlayerInfo()
    {
        crosshair.GetComponent<SpriteRenderer>().color = playerConfig.PlayerColor;
        playerSprite.GetComponentInChildren<Outline>().OutlineColor = playerConfig.PlayerColor;
    }
}
