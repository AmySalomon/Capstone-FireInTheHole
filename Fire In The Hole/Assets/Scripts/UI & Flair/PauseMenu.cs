using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu;

    private void Start()
    {

        pauseMenu.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void UnpauseGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        PlayerPause.paused = false;
        PlayerPause.playerPaused = null;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("MainMenu");
        //Destroy game timer and level intializer objects so they're not floating around on the main menu
        GameObject[] GTObject;
        GTObject = GameObject.FindGameObjectsWithTag("Timer");
        foreach (GameObject timer in GTObject)
            Destroy(timer);
        Debug.Log("[PauseMenu]: Destroyed Game Timer.");
        GameObject[] LevelInitializers;
        LevelInitializers = GameObject.FindGameObjectsWithTag("LevelInitializer");
        foreach (GameObject levelInits in LevelInitializers)
            Destroy(levelInits);
        Debug.Log("[PauseMenu]: Destroyed LevelInitializer.");
    }
}
