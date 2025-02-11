using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroFlyBy : MonoBehaviour
{
    public GameObject myCamera;

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    private TextMeshProUGUI text;

    private float timer = 0;

    private float firstCameraZPos;
    private float firstCameraXPos;
    private float firstCameraYPos;

    private float newZPos;
    private float newXPos;
    private float newYPos;

    public AnimationCurve aniCurve;

    private bool youSuckFirstFrame = false;

    private float cameraZoomZ;

    private float newVertScale;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        text = GetComponentInChildren<TextMeshProUGUI>();
        text.gameObject.transform.localScale = new Vector3(1, 0, 1);

        firstCameraZPos = myCamera.transform.position.z;
        firstCameraYPos = myCamera.transform.position.y;
        firstCameraXPos = myCamera.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //when using Time.unscaledDeltaTime on the first frame, it starts with a few seconds already, 
        //because its measuring time from start of scene to first actionable frame.
        //this stupid section is necessary to stop that from happening.
        if (youSuckFirstFrame) timer += Time.unscaledDeltaTime;
        else
        {
            youSuckFirstFrame = true;
            Time.timeScale = 0;
        }

        ChangeText();
        //THE FOLLOWING IFS ARE IN REVERSE ORDER, IN ORDER TO PROPERLY ANIMATE AT THE CORRECT TIMES.
        //set camera position and time back to normal before self-destructing
        if (timer > 7.6f)
        {
            myCamera.transform.position = new Vector3(firstCameraXPos, firstCameraYPos, firstCameraZPos);
            Time.timeScale = 1;
            Destroy(gameObject);
        }

        //back to OG position
        else if (timer > 6.1f)
        {
            //-1 to offset the end of the screen
            newZPos = Mathf.Lerp(cameraZoomZ, firstCameraZPos, aniCurve.Evaluate((timer - 6.1f) / 1f));
            newXPos = Mathf.Lerp(spawn4.transform.position.x + 1, firstCameraXPos, aniCurve.Evaluate((timer - 6.1f) / 1f));
            newYPos = Mathf.Lerp(spawn4.transform.position.y + 1, firstCameraYPos, aniCurve.Evaluate((timer - 6.1f) / 1f));
            myCamera.transform.position = new Vector3(newXPos, newYPos, newZPos);
        }

        //to player4
        else if (timer > 4.6f)
        {
            //-1 to offset the end of the screen
            newXPos = Mathf.Lerp(spawn3.transform.position.x - 1, spawn4.transform.position.x + 1, aniCurve.Evaluate((timer - 4.6f) / 1f));
            newYPos = Mathf.Lerp(spawn3.transform.position.y - 1, spawn4.transform.position.y + 1, aniCurve.Evaluate((timer - 4.6f) / 1f));
            myCamera.transform.position = new Vector3(newXPos, newYPos, newZPos);
        }

        //to player3
        else if (timer > 3.1f)
        {
            //-1 to offset the end of the screen
            newXPos = Mathf.Lerp(spawn2.transform.position.x - 1, spawn3.transform.position.x - 1, aniCurve.Evaluate((timer - 3.1f) / 1f));
            newYPos = Mathf.Lerp(spawn2.transform.position.y + 1, spawn3.transform.position.y - 1, aniCurve.Evaluate((timer - 3.1f) / 1f));
            myCamera.transform.position = new Vector3(newXPos, newYPos, newZPos);
        }

        //to player2
        else if (timer > 1.6f)
        {
            cameraZoomZ = myCamera.transform.position.z;
            //-1 to offset the end of the screen
            newXPos = Mathf.Lerp(spawn1.transform.position.x + 1, spawn2.transform.position.x - 1, aniCurve.Evaluate((timer - 1.6f) / 1f));
            newYPos = Mathf.Lerp(spawn1.transform.position.y - 1, spawn2.transform.position.y + 1, aniCurve.Evaluate((timer - 1.6f) / 1f));
            myCamera.transform.position = new Vector3(newXPos, newYPos, newZPos);
        }

        //to player 1
        else if (timer > 0.1f)
        {
            //zooms the camera in
            newZPos = Mathf.Lerp(firstCameraZPos, -3, aniCurve.Evaluate((timer - 0.1f) / 1f));
            //+1 to offset the end of the screen
            newXPos = Mathf.Lerp(firstCameraXPos, spawn1.transform.position.x + 1, aniCurve.Evaluate((timer - 0.1f) / 1f));
            newYPos = Mathf.Lerp(firstCameraYPos, spawn1.transform.position.y - 1, aniCurve.Evaluate((timer - 0.1f) / 1f));
            myCamera.transform.position = new Vector3(newXPos, newYPos, newZPos);
        }
    }

    void ChangeText()
    {
        if (timer > 5.1f)
        {
            text.text = "GOLF!";
            newVertScale = Mathf.Lerp(0, 1.3f, (timer - 5.1f) / 0.3f);
            text.gameObject.transform.localScale = new Vector3(newVertScale, newVertScale, 1);
        }

        else if (timer > 4.7f)
        {
            newVertScale = Mathf.Lerp(1, 0, (timer - 4.7f) / 0.3f);
            text.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);
        }

        else if (timer > 2.6f)
        {
            text.text = "SET";
            newVertScale = Mathf.Lerp(0, 1, (timer - 2.6f) / 0.3f);
            text.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);
        }

        else if (timer > 2.2f)
        {
            newVertScale = Mathf.Lerp(1, 0, (timer - 2.2f) / 0.3f);
            text.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);
        }

        else if (timer > 0.1f)
        {
            newVertScale = Mathf.Lerp(0, 1, (timer - 0.1f) / 0.3f);
            text.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);
        }
    }
}
