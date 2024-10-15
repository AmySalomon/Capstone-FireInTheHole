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
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void RandomLevel()
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
            Debug.LogError("LevelSelectManager: Randomizer Broke.");
        }
    }

    public void Blacklight()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Blacklight Resized");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Dinosaur()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Dinosaur Resized");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Volcano()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Volcano Resized");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Pirate()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Pirate Resized");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}

