using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public Sprite bunny;
    public Sprite deadBunny;
    public Sprite walrus;
    public Sprite deadWalrus;
    public Sprite newt;
    public Sprite deadNewt;
    public Sprite duck;
    public Sprite deadDuck;

    private SpriteRenderer mySpriteRenderer;

    public float risingSpeed = 1;
    public float knockbackSpeed = 1;

    [HideInInspector] public Sprite receivedSprite;

    [HideInInspector] public Vector2 knockbackDirection;


    private float timer;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        if(receivedSprite == bunny)
        {
            mySpriteRenderer.sprite = deadBunny;
        }
        if (receivedSprite == newt)
        {
            mySpriteRenderer.sprite = deadNewt;
        }
        if (receivedSprite == walrus)
        {
            mySpriteRenderer.sprite = deadWalrus;
        }
        if (receivedSprite == duck)
        {
            mySpriteRenderer.sprite = deadDuck;
        }

        originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 0.13f)
        {
            transform.position = new Vector3(originalPosition.x + Random.Range(-.1f, .1f), originalPosition.y + Random.Range(-.1f, .1f));
        }
        else
        {
            StartCoroutine(DestroySelf());
            transform.position += new Vector3(-knockbackDirection.normalized.x, -knockbackDirection.normalized.y, -risingSpeed) * Time.deltaTime * knockbackSpeed;
            transform.Rotate(0, 0, 3);
        }
        
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
