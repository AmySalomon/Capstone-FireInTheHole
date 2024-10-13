using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePicker : MonoBehaviour
{
    public PlayerInputHandler myInputHandler;

    private SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = myInputHandler.playerSprite;
    }

}
