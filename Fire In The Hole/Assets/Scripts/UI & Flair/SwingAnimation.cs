using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is a hacky quick way of getting a sprite animation without having to fuck with the unity animator, which is too complex to justify for something this simple.
//made by Amy.
public class SwingAnimation : MonoBehaviour
{
    public Sprite Frame1;
    public Sprite Frame2;
    public Sprite Frame3;
    public Sprite Frame4;
    public Sprite Frame5;
    public Sprite Frame6;
    public Sprite Frame7;

    private float timer;

    private bool doSwing;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private float swingDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (doSwing)
            timer += Time.deltaTime;
        {
            if (timer > swingDuration)
            {
                doSwing = false;
                spriteRenderer.enabled = false;
                timer = 0;
            }
            else if (timer > swingDuration / 7 * 6) spriteRenderer.sprite = Frame7;
            else if (timer > swingDuration / 7 * 5) spriteRenderer.sprite = Frame6;
            else if (timer > swingDuration / 7 * 4) spriteRenderer.sprite = Frame5;
            else if (timer > swingDuration / 7 * 3) spriteRenderer.sprite = Frame4;
            else if (timer > swingDuration / 7 * 2) spriteRenderer.sprite = Frame3;
            else if (timer > swingDuration / 7 * 1) spriteRenderer.sprite = Frame2;
            else spriteRenderer.sprite = Frame1;
        }
    }

    //swing script will reference this, telling this script to do the animation, and resetting it if it's in progress.
    public void DoAnimation()
    {
        timer = 0;
        doSwing = true;
        spriteRenderer.enabled = true;
    }
}
