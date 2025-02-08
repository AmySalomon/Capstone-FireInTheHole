using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGunSprite : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Transform gunRotation;

    public bool isFlipped;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gunRotation.eulerAngles);
        if (gunRotation.eulerAngles.z >= 90 && gunRotation.eulerAngles.z <= 270)
        {
            sprite.flipY = true;
            isFlipped = false;
            transform.localPosition = new Vector3(-0.8f, 0.17f, 0);
        }

        else
        {
            sprite.flipY = false;
            isFlipped = true;
            transform.localPosition = new Vector3(-0.8f, -0.17f, 0);
        }
    }
}
