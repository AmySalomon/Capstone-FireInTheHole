using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallImpactAnimation : MonoBehaviour
{
    public Sprite Frame1;
    public Sprite Frame2;
    public Sprite Frame3;

    private float timer;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private float animDuration = 0.7f;

    private float newVertScale;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > animDuration)
        {
            Destroy(gameObject);
        }
        else if (timer > animDuration / 3 * 2) spriteRenderer.sprite = Frame3;
        else if (timer > animDuration / 3 * 1) spriteRenderer.sprite = Frame2;
        else spriteRenderer.sprite = Frame1;

        if (timer < animDuration)
        {
            newVertScale = Mathf.Lerp(0.08f, 0, timer / (animDuration * 3));
            transform.localScale = new Vector3(newVertScale, 0.08f, 0.08f);
        }

    }
}
