using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    //Random Play Level
    public int randomLevel;

    //Outputs chosen level name
    public string chosenLevel;

    //For Loading and Destroying
    public static LevelSelectManager LSManager;

    public string playerSetup;

    private void Awake()
    {
        if (LSManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            LSManager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void BackToMain()
    {
        Time.timeScale = 1f;
        chosenLevel = "Main Menu (NOT WORKING YET)";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("LevelSelect");
    }

    public void LevelRandom()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        randomLevel = Random.Range(1, 5); //Random Level Generator
        Debug.Log(randomLevel);
        if (randomLevel == 1)
        {
            chosenLevel = "3D Test (Blacklight)";
            SceneManager.LoadScene(playerSetup);
        }
        else if (randomLevel == 2)
        {
            chosenLevel = "Dinosaur Resized";
            SceneManager.LoadScene(playerSetup);
        }
        else if (randomLevel == 3)
        {
            chosenLevel = "Volcano Resized";
            SceneManager.LoadScene(playerSetup);
        }
        else if (randomLevel == 4)
        {
            chosenLevel = "Pirate Resized";
            SceneManager.LoadScene(playerSetup);
        }
        else
        {
            Debug.LogError("LevelSelectManager: Randomizer Broke. (LevelRandom)");
        }
    }

    public void LevelBlacklight()
    {
        Time.timeScale = 1f;
        chosenLevel = "3D Test (Blacklight)";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);
    }

    public void LevelDinosaur()
    {
        Time.timeScale = 1f;
        chosenLevel = "Dinosaur Resized";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);
    }

    public void LevelVolcano()
    {
        Time.timeScale = 1f;
        chosenLevel = "Volcano Resized";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);
    }
        

    public void LevelPirate()
    {
        Time.timeScale = 1f;
        chosenLevel = "Pirate Resized";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);
        
    }
}

