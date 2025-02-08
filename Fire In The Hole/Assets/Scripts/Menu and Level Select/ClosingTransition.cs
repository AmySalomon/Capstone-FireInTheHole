using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosingTransition : MonoBehaviour
{
    //this script advances through the splash screen
    private float timer = 0;

    //soundPlayer
    private AudioSource audioPlayer;

    //Dies audio
    public AudioClip playerKilled;
    private bool dead = false;

    //Start
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.1)
        {
            if (dead == false)
            {
                audioPlayer.PlayOneShot(playerKilled, 1f);
                dead = true;
            }
        }
    }
}
