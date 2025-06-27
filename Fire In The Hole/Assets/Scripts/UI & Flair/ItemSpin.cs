using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{

    public AnimationCurve aniCurve;

    public GameObject mySprite;

    private float timer = 0;
    public float timeForFullSpin = 2;

    private float YrotationValue;

    private bool isFlipped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < timeForFullSpin && !isFlipped)
        {
            YrotationValue = Mathf.Lerp(0, 180, aniCurve.Evaluate(timer / timeForFullSpin));
            mySprite.transform.eulerAngles = new Vector3(0, YrotationValue, 0);
        }
        else if (timer < timeForFullSpin && isFlipped)
        {
            YrotationValue = Mathf.Lerp(180, 0, aniCurve.Evaluate(timer / timeForFullSpin));
            mySprite.transform.eulerAngles = new Vector3(0, YrotationValue, 0);
        }
        else if (isFlipped)
        {
            //if the lerp has ended, snap the rotation to the full turn, and then restart it
            mySprite.transform.eulerAngles = new Vector3(0, 0, 0);
            timer = 0;
            isFlipped = !isFlipped;
        }
        else if (!isFlipped)
        {
            //if the lerp has ended, snap the rotation to the full turn, and then restart it
            mySprite.transform.eulerAngles = new Vector3(0, 180, 0);
            timer = 0;
            isFlipped = !isFlipped;
        }
    }
}
