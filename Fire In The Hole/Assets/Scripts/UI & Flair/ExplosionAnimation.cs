using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public Sprite frame2;
    public Sprite frame3;
    public Sprite frame4;
    public Sprite frame5;
    public Sprite frame6;
    public Sprite frame7;
    public Sprite frame8;

    private SpriteRenderer mySprite;

    private float timer;
    public float interval;

    public GameObject explosionCreator;
    private void Start()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //there are currently 8 frames in the animation, so it goes 8 times.
        //yes I hard coded this, because I hate unity's animator with a passion when it comes to simple animations like this
        if (timer >= interval * 8) Destroy(gameObject);
        else if (timer >= interval * 7) mySprite.sprite = frame8;
        else if (timer >= interval * 6) mySprite.sprite = frame7;
        else if (timer >= interval * 5) mySprite.sprite = frame6;
        else if (timer >= interval * 4) mySprite.sprite = frame5;
        else if (timer >= interval * 3) mySprite.sprite = frame4;
        else if (timer >= interval * 2)
        {
            //turn off the explosion damage after the first frame ends
            GetComponent<CircleCollider2D>().enabled = false;
            mySprite.sprite = frame3;
        }
        else if (timer >= interval) mySprite.sprite = frame2;
    }
}
