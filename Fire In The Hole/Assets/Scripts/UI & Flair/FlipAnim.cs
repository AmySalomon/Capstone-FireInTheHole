using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic tool script, flips between two sprites. simple and easy to use.
public class FlipAnim : MonoBehaviour
{
    public float flipDuration = .5f;

    public Sprite frame1;
    public Sprite frame2;

    private SpriteRenderer mySprite;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < flipDuration / 2) mySprite.sprite = frame1;
        else mySprite.sprite = frame2;

        if (timer > flipDuration) timer = 0;
    }
}
