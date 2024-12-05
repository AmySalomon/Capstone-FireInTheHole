using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        //No duplicate LevelSelectManager!!!
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu")
        {
            GameObject[] LSManager;
            LSManager = GameObject.FindGameObjectsWithTag("LevelSelectManager");
            foreach (GameObject manager in LSManager)
                Destroy(manager);
            Debug.Log("[GameManager]: Destroyed LevelSelectManager.");
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

    }

    public void GoToOptions()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("OptionsMenu");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GoToCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


