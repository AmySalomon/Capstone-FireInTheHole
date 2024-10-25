using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimator : MonoBehaviour
{
    public Sprite flagFrame1;
    public Sprite flagFrame2;
    public Sprite flagFrame3;
    public Sprite flagFrame4;
    public Sprite flagFrame5;
    public Sprite flagFrame6;

    private SpriteRenderer mySprite;

    private float timer;
    public float interval;


    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval * 6)
        {
            mySprite.sprite = flagFrame6;
            timer = 0;
        }
        else if (timer >= interval * 5) mySprite.sprite = flagFrame5;
        else if (timer >= interval * 4) mySprite.sprite = flagFrame4;
        else if (timer >= interval * 3) mySprite.sprite = flagFrame3;
        else if (timer >= interval * 2) mySprite.sprite = flagFrame2;
        else if (timer >= interval) mySprite.sprite = flagFrame1;
    }
}
