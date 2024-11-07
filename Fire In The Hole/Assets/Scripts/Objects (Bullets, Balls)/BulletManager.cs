using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public BulletType bulletType;
    public float timer;
    public float deletionTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        UpdateBulletType();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= deletionTime)
        {
            DestroyBullet(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bulletType.BulletCollision(collision, this.gameObject);
    }

    public void UpdateBulletType()
    {
        bulletType = ScriptableObject.Instantiate(bulletType);
        deletionTime = bulletType.deletionTime;
    }
    public void DestroyBullet(GameObject bullet)
    {
        bulletType.DeleteBullet(this.gameObject);
    }
}
