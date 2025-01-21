using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoHazard : MonoBehaviour
{
    public float maxRotationDegrees = 35;
    //speed in degrees per second
    public float rotationSpeed = 5f; 
    //the two ends of the rotation axis, where it starts and where it ends
    private Quaternion firstPosition;
    private Quaternion endPosition;

    public AnimationCurve rotationCurve;

    //whether or not the rotation is going from 1st to 2nd (true), or reverse (false)
    private bool goingToEndPos = true;
    //whether or not this hazard is currently idle or spitting the ball
    private bool tryingToSpit = false;


    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.rotation;
        endPosition = Quaternion.Euler(0, 0,firstPosition.eulerAngles.z + maxRotationDegrees);
    }

    // Update is called once per frame
    void Update()
    {
        //if going to end AND not trying to spit a ball, then go to end.
        if (goingToEndPos && !tryingToSpit)
        {
            //degrees we must travel
            var angle = Quaternion.Angle(firstPosition, endPosition); 
            //angle/rotationspeed = time it'd take to travel a at such speed
            StartCoroutine(RotateToEnd(angle / rotationSpeed));
        }
        else if (!goingToEndPos && !tryingToSpit)
        {
            var angle = Quaternion.Angle(endPosition, firstPosition);
            StartCoroutine(RotateToFirst(angle / rotationSpeed));
        }
    }

    IEnumerator RotateToEnd(float duration)
    {
        float time = 0f;
        while(time < duration)
        {
            transform.rotation = Quaternion.Slerp(firstPosition, endPosition, rotationCurve.Evaluate(time / duration));
            
            yield return null;
            time += Time.deltaTime;
        }
        transform.rotation = endPosition;
        goingToEndPos = false;
        StopAllCoroutines();
    }


    IEnumerator RotateToFirst(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(endPosition, firstPosition, rotationCurve.Evaluate(time / duration));
            
            yield return null;
            time += Time.deltaTime;
        }
        transform.rotation = firstPosition;
        goingToEndPos = true;
        StopAllCoroutines();
    }
}
