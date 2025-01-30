using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTurret : MonoBehaviour
{
    public float shotDelay = .5f;
    public float muzzleFlashDuration = 0.1f;
    public float launchForce = 2f;

    //gets bullet prefab
    public Rigidbody2D bullet;
    //gets barrel position
    public Transform barrelEnd;
    //muzzle flash sprite
    public SpriteRenderer muzzleFlash;

    //timer variable
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > muzzleFlashDuration)
        {
            muzzleFlash.enabled = false;
        }

        if (time > shotDelay)
        {
            muzzleFlash.enabled = true;
            Rigidbody2D newBullet = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
            newBullet.velocity = new Vector2(0, -launchForce);
            time = 0;
        }
    }
}
