using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public double timer = 0.25;
    
    private void Update()
    {//go away after timer elapses
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            //GetComponent<BulletManager>().DestroyBullet();
            Destroy(this.gameObject);
        }
    }
}
