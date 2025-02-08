using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PirateWaterSound : MonoBehaviour
{
    //this script adds the water warning sounds
    
    //Timer stuff
    private float currentTime = 0;
    private float tempTime = 0;
    public float tideInDuration;
    public float tideOutDuration;
    public float sirenDuration;

    //soundPlayer
    private AudioSource audioPlayer;

    //Water audio
    public AudioClip tideInSound;
    public AudioClip tideOutSound;
    public AudioClip sirenSound;

    //Check state
    public bool tideIn = true;
    public bool siren = true;

    //Start
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        //Siren sound effect
        if (currentTime > tempTime + sirenDuration)
        {
            if (siren == true)
            {
                audioPlayer.PlayOneShot(sirenSound, 1f);
                //Debug.Log("Siren Sound+++++++++++++++++");
                siren = false;
            }
        }

        //Tide In sound effect
        if (currentTime > tempTime + tideInDuration)
        {
            if (tideIn == true)
            {
                audioPlayer.PlayOneShot(tideInSound, 1f);
                //Debug.Log("Tide In Sound+++++++++++++++++");
                tideIn = false;
                tempTime = currentTime + 1;
            }
        }

        //Tide Out sound effect
        else if (currentTime > tempTime + tideOutDuration)
        {
            if (tideIn == false)
            {
                audioPlayer.PlayOneShot(tideOutSound, 1f);
                //Debug.Log("Tide Out Sound++++++++++++");
                tideIn = true;
                siren = true;
                tempTime = currentTime + 1;
            }
        }
    }
}
