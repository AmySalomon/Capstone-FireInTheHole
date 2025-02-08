using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleAnimation : MonoBehaviour
{
    private SpriteRenderer mySprite;

    public Sprite frame1;
    public Sprite frame3;
    public Sprite frame5;

    public float duration = .15f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            mySprite.enabled = false;
            timer = 0;
        }
        //i decided to only use 3 frames. half the frames are essentially duplicates that make it go on for too long.
        else if (timer < duration / 3 * 2) mySprite.sprite = frame5;
        else if (timer < duration / 3 * 1) mySprite.sprite = frame3;
        else mySprite.sprite = frame1;
    }

    public void StartAnimation()
    {
        timer = duration;
        mySprite.enabled = true;
    }
}
