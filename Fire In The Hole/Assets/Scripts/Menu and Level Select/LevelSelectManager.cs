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
        SceneManager.LoadScene("LevelSelect");
        chosenLevel = "Main Menu (NOT WORKING YET)";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
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
            SceneManager.LoadScene("Blacklight Resized");
            chosenLevel = "Blacklight Resized";
        }
        else if (randomLevel == 2)
        {
            SceneManager.LoadScene("Dinosaur Resized");
            chosenLevel = "Dinosaur Resized";
        }
        else if (randomLevel == 3)
        {
            SceneManager.LoadScene("Volcano Resized");
            chosenLevel = "Volcano Resized";
        }
        else if (randomLevel == 4)
        {
            SceneManager.LoadScene("Pirate Resized");
            chosenLevel = "Pirate Resized";
        }
        else
        {
            Debug.LogError("LevelSelectManager: Randomizer Broke. (LevelRandom)");
        }
    }

    public void LevelBlacklight()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Blacklight Resized");
        chosenLevel = "Blacklight Resized";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void LevelDinosaur()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Dinosaur Resized");
        chosenLevel = "Dinosaur Resized";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void LevelVolcano()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Volcano Resized");
        chosenLevel = "Volcano Resized";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void LevelPirate()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Pirate Resized");
        chosenLevel = "Pirate Resized";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}

