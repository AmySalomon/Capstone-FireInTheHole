using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class OptionsMenuManager : MonoBehaviour
{

    //Access Toggle Option
    public static bool noVibrate;

    //Volume Sliders
    public Slider masterVol, musicVol, soundVol, voiceVol;
    public float masterVolTemp, musicVolTemp, soundVolTemp, voiceVolTemp;
    public AudioMixer mainAudioMixer;

    //Vibrate Toggle
    public Toggle vibrateToggle;
    private bool toggleIsSet = false;
    private bool rumbling = false;

    //Transition
    public Animator transition;
    public float transitionTime;
    public EventSystem eventSystem;

    //Change Audio Sliders
    public void ChangeMasterVolume()
    {
        if (masterVol.value <= -20) //The lowest setting 
        {
            mainAudioMixer.SetFloat("MasterParam", -80);
        }
        else
        {
            mainAudioMixer.SetFloat("MasterParam", masterVol.value);
        }
        masterVolTemp = masterVol.value;
        PlayerPrefs.SetFloat("MasterParam", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        if (musicVol.value <= -20) //The lowest setting 
        {
            mainAudioMixer.SetFloat("MusicParam", -80);
        }
        else
        {
            mainAudioMixer.SetFloat("MusicParam", musicVol.value);
        }
        musicVolTemp = musicVol.value;
        PlayerPrefs.SetFloat("MusicParam", musicVol.value);

    }

    public void ChangeSoundVolume()
    {
        if (soundVol.value <= -20) //The lowest setting 
        {
            mainAudioMixer.SetFloat("SoundParam", -80);
        }
        else
        {
            mainAudioMixer.SetFloat("SoundParam", soundVol.value);
        }
        soundVolTemp = soundVol.value;
        PlayerPrefs.SetFloat("SoundParam", soundVol.value);

    }

    public void ChangeVoiceVolume()
    {
        if (voiceVol.value <= -20) //The lowest setting 
        {
            mainAudioMixer.SetFloat("VoiceParam", -80);
        }
        else
        {
            mainAudioMixer.SetFloat("VoiceParam", voiceVol.value);
        }
        voiceVolTemp = voiceVol.value;
        PlayerPrefs.SetFloat("VoiceParam", voiceVol.value);

    }

    //Change Toggle
    public void ChangeVibrate()
    {
        if (toggleIsSet == true)
        {
            if (noVibrate == true)
            {
                noVibrate = false;
            }
            else if (noVibrate == false)
            {
                noVibrate = true;
            }
            else
            {
                Debug.Log("[OptionsMenuManager]: Vibrate toggle broke.");
            }
            Debug.Log("noVibrate is " + noVibrate);
        }
        PlayerPrefs.SetString("noVibrate", noVibrate.ToString());
        Debug.Log("In Player Prefs, noVibrate is " + PlayerPrefs.GetString("noVibrate"));
    }

    IEnumerator LoadLevel(string nextScene) //Transition
    {
        eventSystem.SetSelectedGameObject(null);
        transition.SetTrigger("Exit");

        yield return new WaitForSeconds(transitionTime);

        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Start()
    {
        Debug.Log("noVibrate is " + noVibrate);
        //Match toggle visuals to actual toggle.
        if (noVibrate == false)
        {
            vibrateToggle.isOn = true;
        }
        else if (noVibrate == true)
        {
            vibrateToggle.isOn = false;
        }
        else
        {
            Debug.Log("[OptionsMenuManager]: (In Start) Vibrate toggle broke.");
        }

        toggleIsSet = true;
        //Match Audio Sliders to Audio Mixer
        mainAudioMixer.GetFloat("MasterParam", out float master);
        masterVol.value = master;
        mainAudioMixer.GetFloat("MusicParam", out float music);
        musicVol.value = music;
        mainAudioMixer.GetFloat("SoundParam", out float sound);
        soundVol.value = sound;
        mainAudioMixer.GetFloat("VoiceParam", out float voice);
        voiceVol.value = voice;

        
    }

    //Return to level select.
    public void LevelSelect()
    {
        /*Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("LevelSelect");*/
        StartCoroutine(LoadLevel("LevelSelect"));
    }

    //Return to main menu.
    public void MainMenu()
    {
        /*Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("MainMenu");*/
        StartCoroutine(LoadLevel("MainMenu"));
    }

    //Kill LevelSelectManager!!!
    public void Awake()
    {
        GameObject[] PMObject;
        PMObject = GameObject.FindGameObjectsWithTag("LevelSelectManager");
        foreach (GameObject manager in PMObject)
           Destroy(manager);
        Debug.Log("[OptionsMenuManager]: Destroyed LevelSelectManager.");
    }
}
