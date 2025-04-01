using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAnim : MonoBehaviour
{
    public Sprite shieldFrame3;
    public Sprite shieldFrame4;
    public Sprite shieldFrame5;
    public Sprite shieldFrame6;
    public Sprite shieldFrame7;

    private SpriteRenderer mySprite;

    [HideInInspector] public float timer;
    public float interval;


    private void Start()
    {
        timer = 10;
        mySprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval * 6) mySprite.sprite = null;
        else if (timer >= interval * 5) mySprite.sprite = shieldFrame7;
        else if (timer >= interval * 4) mySprite.sprite = shieldFrame6;
        else if (timer >= interval * 3) mySprite.sprite = shieldFrame5;
        else if (timer >= interval * 2) mySprite.sprite = shieldFrame4;
        else if (timer >= interval) mySprite.sprite = shieldFrame3;
    }
}