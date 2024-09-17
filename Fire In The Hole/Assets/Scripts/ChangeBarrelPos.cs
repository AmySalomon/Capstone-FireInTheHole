using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBarrelPos : MonoBehaviour
{
    public FlipGunSprite gunSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gunSprite.isFlipped == true)
        {
            transform.localPosition = new Vector3(-2.7f, 0.43f, 0);
        }

        else
        {
            transform.localPosition = new Vector3(-2.7f, -0.43f, 0);
        }
    }
}
