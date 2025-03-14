using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAnim : MonoBehaviour
{
    [HideInInspector] public bool isRunning = false;
    private Sprite OGSprite;
    public Sprite bunnyRun1;
    public Sprite bunnyRun2;
    public Sprite bunnyRun3;
    public Sprite bunnyRun4;
    public Sprite duckRun1;
    public Sprite duckRun2;
    public Sprite duckRun3;
    public Sprite duckRun4;
    public Sprite newtRun1;
    public Sprite newtRun2;
    public Sprite newtRun3;
    public Sprite newtRun4;
    public Sprite walrusRun1;
    public Sprite walrusRun2;
    public Sprite walrusRun3;
    public Sprite walrusRun4;

    private SpriteRenderer mySpriteRenderer;

    private bool singleCheck = false;

    private float timer = 0;
    private float interval = 0.1f;

    void Start()
    {
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            //this is required to properly set the sprite to whatever the player picked (if placed in start, it gets the placeholder sprite)
            if (singleCheck == false)
            {
                OGSprite = mySpriteRenderer.sprite;
                singleCheck = true;
            }

            timer += Time.deltaTime;
            
            if (mySpriteRenderer.sprite.name.Contains("bunny"))
            {
                if (timer >= interval * 4) timer = 0;
                else if (timer >= interval * 3) mySpriteRenderer.sprite = bunnyRun4;
                else if (timer >= interval * 2) mySpriteRenderer.sprite = bunnyRun3;
                else if (timer >= interval) mySpriteRenderer.sprite = bunnyRun2;
                else mySpriteRenderer.sprite = bunnyRun1;
            }
            if (mySpriteRenderer.sprite.name.Contains("duck"))
            {
                if (timer >= interval * 4) timer = 0;
                else if (timer >= interval * 3) mySpriteRenderer.sprite = duckRun4;
                else if (timer >= interval * 2) mySpriteRenderer.sprite = duckRun3;
                else if (timer >= interval) mySpriteRenderer.sprite = duckRun2;
                else mySpriteRenderer.sprite = duckRun1;
            }
            if (mySpriteRenderer.sprite.name.Contains("walrus"))
            {
                if (timer >= interval * 4) timer = 0;
                else if (timer >= interval * 3) mySpriteRenderer.sprite = walrusRun4;
                else if (timer >= interval * 2) mySpriteRenderer.sprite = walrusRun3;
                else if (timer >= interval) mySpriteRenderer.sprite = walrusRun2;
                else mySpriteRenderer.sprite = walrusRun1;
            }
            if (mySpriteRenderer.sprite.name.Contains("newt"))
            {
                if (timer >= interval * 4) timer = 0;
                else if (timer >= interval * 3) mySpriteRenderer.sprite = newtRun4;
                else if (timer >= interval * 2) mySpriteRenderer.sprite = newtRun3;
                else if (timer >= interval) mySpriteRenderer.sprite = newtRun2;
                else mySpriteRenderer.sprite = newtRun1;
            }
        }

        else
        {
            timer = 0;
            if (OGSprite != null) mySpriteRenderer.sprite = OGSprite;
        }
        
    }
}
