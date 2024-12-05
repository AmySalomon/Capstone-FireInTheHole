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
        GameObject[] LevelInitializers;
        LevelInitializers = GameObject.FindGameObjectsWithTag("LevelInitializer");
        foreach (GameObject timer in LevelInitializers)
            Destroy(timer);
        Debug.Log("[PauseMenu]: LevelInitializer.");
    }
}
