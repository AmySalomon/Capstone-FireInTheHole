using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    public static MenuMusic bgmusic; //For keeping music between scenes
    private static string currentLevelName = "Null"; // Hold the current level scene
    private bool menuMusic = true;

    private void Awake()
    {
        if (menuMusic)
        {
            if (bgmusic != null)
            {
                Destroy(gameObject);
            }
            else
            {
                bgmusic = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        SceneNameUpdate();
    }

    void Update()
    {
        SceneNameUpdate();
        if (currentLevelName == null)
        {
            Debug.Log("Scene name not found.");
        }
        else if (currentLevelName == "MainMenu" || currentLevelName == "OptionsMenu" || currentLevelName == "LevelSelect" || currentLevelName == "Credits")
        {
            menuMusic = true;
        }
        else
        {
            menuMusic = false;
            Destroy(gameObject);
        }
    }

    void SceneNameUpdate() //Update the scene name
    {
        // Get the current scene name
        Scene currentLevel = SceneManager.GetActiveScene();
        currentLevelName = currentLevel.name;
        //Debug.Log("Loaded Level: " + currentLevel.name);
    }
}
