using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerPrefs();
    }


    public void LoadPlayerPrefs()
    {
        var masterVolume = PlayerPrefs.GetFloat("MasterParam", 0);
        audioMixer.SetFloat("MasterParam", masterVolume);

        var musicVolume = PlayerPrefs.GetFloat("MusicParam", 0);
        audioMixer.SetFloat("MusicParam", musicVolume);

        var soundVolume = PlayerPrefs.GetFloat("SoundParam", 0);
        audioMixer.SetFloat("SoundParam", soundVolume);

        var voiceVolume = PlayerPrefs.GetFloat("VoiceParam", 0);
        audioMixer.SetFloat("VoiceParam", voiceVolume);

        var noVibrate = PlayerPrefs.GetString("noVibrate", "False");
        if (noVibrate == "False") 
        { 
            OptionsMenuManager.noVibrate = false;
            Debug.Log("Ring ring");
        }
        else if (noVibrate == "True") 
        { 
            OptionsMenuManager.noVibrate = true;
            Debug.Log("bong bonk");
        }
        else
        {
            Debug.Log("Nothing Worked");
        }
        Debug.Log("noVibrate is "+OptionsMenuManager.noVibrate);
        Debug.Log("In Player Prefs, noVibrate is " + PlayerPrefs.GetString("noVibrate"));

        //var toggleIsSet = PlayerPrefs.GetString("toggleIsSet", "false");
        //if(toggleIsSet == "false") {OptionsMenuManager.toggleIsSet = false; }
        //else if(toggleIsSet == "true") { OptionsMenuManager.toggleIsSet = true; }

    }

    public void SavePlayerPrefs()
    {
    }
}
