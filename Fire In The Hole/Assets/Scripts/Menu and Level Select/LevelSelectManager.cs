using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    //Random Play Level
    public int randomLevel;

    public void BackToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
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
        }
        else if (randomLevel == 2)
        {
            SceneManager.LoadScene("Dinosaur Resized");
        }
        else if (randomLevel == 3)
        {
            SceneManager.LoadScene("Volcano Resized");
        }
        else if (randomLevel == 4)
        {
            SceneManager.LoadScene("Pirate Resized");
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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void LevelDinosaur()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Dinosaur Resized");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void LevelVolcano()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Volcano Resized");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void LevelPirate()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Pirate Resized");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}

