using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenuManager : MonoBehaviour
{
    //Volume Sliders
    public Slider masterVol, musicVol, soundVol;
    public float masterVolTemp, musicVolTemp, soundVolTemp;
    public AudioMixer mainAudioMixer;

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
    }

    void Start()
    {
        //Match Audio Sliders to Audio Mixer
        mainAudioMixer.GetFloat("MasterParam", out float master);
        masterVol.value = master;
        mainAudioMixer.GetFloat("MusicParam", out float music);
        musicVol.value = music;
        mainAudioMixer.GetFloat("SoundParam", out float sound);
        soundVol.value = sound;
    }

    //Return to level select.
    public void LevelSelect()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("LevelSelect");
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
