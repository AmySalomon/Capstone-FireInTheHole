using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public AudioClip niceShot1;
    public AudioClip niceShot2;
    public AudioClip niceShot3;

    public float timeToPopup = 2;
    public float timeToDelete = 3;

    private float time;

    private Vector3 startPosition;
    private Vector3 endPosition;

    public float yModifier = 1;
    private float newYValue;

    private TextMeshProUGUI text;
    private AudioSource audio;

    [HideInInspector] public Color myColor;
    private float newOpacityValue;
    private float startOpacity = 1;
    private float endOpacity = 0;

    [HideInInspector] public string weaponPickup;
    [HideInInspector] public string powerupPickup;

    public GameObject confetti;
    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        audio = GetComponent<AudioSource>();
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, transform.position.y + yModifier, transform.position.z);
        if (weaponPickup == "None")
        {
            int whichClip = Random.Range(1, 4);
            switch (whichClip)
            {
                case 1:
                    audio.PlayOneShot(niceShot1);
                    break;
                case 2:
                    audio.PlayOneShot(niceShot2);
                    break;
                case 3:
                    audio.PlayOneShot(niceShot3);
                    break;
                default:
                    audio.PlayOneShot(niceShot1);
                    break;
            }
            text.text = "Nice Shot!";

            Instantiate(confetti, startPosition, Quaternion.identity);
        }

        else


        if (weaponPickup == "Shotgun")
        {
            text.text = "Shotgun";
            myColor = new Color(1, .6f, 0);
        }

        if (weaponPickup == "Sniper") 
        {
            text.text = "Sniper";
            myColor = new Color(1, .6f, 0);
        }

        if (weaponPickup == "TommyGun")
        {
            text.text = "SMG";
            myColor = new Color(1, .6f, 0);
        }

        if (weaponPickup == "RocketLauncher")
        {
            text.text = "Rocket";
            myColor = new Color(1, .6f, 0);
        }

        if (weaponPickup == "PU_speed Boost")
        {
            text.text = "Speed Up";
            myColor = new Color(0.61f, 0.2f, 0.92f);
        }
        if (weaponPickup == "PU_bouncyBullet")
        {
            text.text = "Ricochet";
            myColor = new Color(0.61f, 0.2f, 0.92f);
        }
        Color colorTop = new Color(myColor.r, myColor.g, myColor.b);
        text.colorGradient = new VertexGradient(colorTop, colorTop, Color.white, Color.white);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time < timeToPopup)
        {
            newYValue = Mathf.Lerp(startPosition.y, endPosition.y, animationCurve.Evaluate(time / timeToPopup));
            transform.position = new Vector3(transform.position.x, newYValue, transform.position.z);
        }
        else if (time < timeToDelete)
        {
            //if the lerp has ended, snap the circle to have the normal scale
            transform.position = new Vector3(transform.position.x, endPosition.y, transform.position.z);
            newOpacityValue = Mathf.Lerp(startOpacity, endOpacity, (time - timeToPopup) / (timeToDelete - timeToPopup));
            text.color = new Color(1, 1, 1, newOpacityValue);

        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
