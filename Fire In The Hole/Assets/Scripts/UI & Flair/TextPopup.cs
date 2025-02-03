using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    public AnimationCurve animationCurve;

    public float timeToPopup = 2;
    public float timeToDelete = 3;

    private float time;

    private Vector3 startPosition;
    private Vector3 endPosition;

    public float yModifier = 1;
    private float newYValue;

    private TextMeshProUGUI text;

    [HideInInspector] public Color myColor;
    private float newOpacityValue;
    private float startOpacity = 1;
    private float endOpacity = 0;

    [HideInInspector] public string weaponPickup;
    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, transform.position.y + yModifier, transform.position.z);
        if (weaponPickup == "None") text.text = "Nice Shot!";

        else myColor = new Color(1, .6f, 0);


        if (weaponPickup == "Shotgun") text.text = "Shotgun";


        if (weaponPickup == "Sniper") text.text = "Sniper";


        if (weaponPickup == "TommyGun") text.text = "SMG";

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
