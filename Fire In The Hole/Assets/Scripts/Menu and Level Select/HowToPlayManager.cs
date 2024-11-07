using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    private void Awake()
    {
        //No duplicate LevelSelectManager!!!
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "HowTo (Test)")
        {
            GameObject[] LSManager;
            LSManager = GameObject.FindGameObjectsWithTag("LevelSelectManager");
            foreach (GameObject manager in LSManager)
                Destroy(manager);
            Debug.Log("[HowToPlayManager]: Destroyed LevelSelectManager.");
        }

        //No Timer! Or LevelSelectManager!
        if (currentScene == "EndingScene")
        {
            GameObject[] LSManager;
            LSManager = GameObject.FindGameObjectsWithTag("LevelSelectManager");
            foreach (GameObject manager in LSManager)
                Destroy(manager);
            Debug.Log("[HowToPlayManager]: Destroyed LevelSelectManager.");

            GameObject[] GameTimer;
            GameTimer = GameObject.FindGameObjectsWithTag("Timer");
            foreach (GameObject timer in GameTimer)
                Destroy(timer);
            Debug.Log("[HowToPlayManager]: Destroyed GameTimer.");
        }
    }

    public void BackToSelect()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("LevelSelect");
    }
}

