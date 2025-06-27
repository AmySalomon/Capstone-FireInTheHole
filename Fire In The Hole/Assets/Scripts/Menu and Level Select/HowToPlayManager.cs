using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HowToPlayManager : MonoBehaviour
{
    //Transition
    public Animator transition;
    public float transitionTime;
    public EventSystem eventSystem;

    private void Awake()
    {
        //No duplicate LevelSelectManager!!!
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "HowToPlay")
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

            GameObject[] PMObject;
            PMObject = GameObject.FindGameObjectsWithTag("PlayerManager");
            foreach (GameObject manager in PMObject)
                Destroy(manager);
            Debug.Log("[HowToPlayManager]: Destroyed Player Manager.");

            //GameObject[] GameTimer;
            //GameTimer = GameObject.FindGameObjectsWithTag("Timer");
            //foreach (GameObject timer in GameTimer)
            //    Destroy(timer);
            //Debug.Log("[HowToPlayManager]: Destroyed GameTimer.");
        }
    }

    IEnumerator LoadLevel(string nextScene) //Transition
    {
        eventSystem.SetSelectedGameObject(null);
        transition.SetTrigger("Exit");

        yield return new WaitForSeconds(transitionTime);

        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void BackToSelect()
    {
        /*Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("LevelSelect");*/
        StartCoroutine(LoadLevel("LevelSelect"));
    }

    public void BackToMain()
    {
        /*Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("MainMenu");*/
        StartCoroutine(LoadLevel("MainMenu"));
    }
}

