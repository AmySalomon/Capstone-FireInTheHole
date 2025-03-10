using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //Quit Panel
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject quitCancelButton;
    [SerializeField] GameObject playButton;
    public EventSystem eventSystem;

    //Transition
    public Animator transition;
    public float transitionTime;

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

    public void PlayGame()
    {
        /*Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;*/

        StartCoroutine(LoadLevel("LevelSelect"));
    }

    public void GoToOptions()
    {
        /*Time.timeScale = 1f;
        SceneManager.LoadScene("OptionsMenu");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;*/

        StartCoroutine(LoadLevel("OptionsMenu"));
    }

    public void GoToCredits()
    {
        /*Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;*/

        StartCoroutine(LoadLevel("Credits"));
    }

    public void GoToLevelSelect()
    {
        /*Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;*/

        StartCoroutine(LoadLevel("LevelSelect"));
    }

    public void GoToMain()
    {
        /*Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;*/

        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void AreYouSureQuit()
    {
        quitPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(quitCancelButton);
    }

    public void CancelQuit()
    {
        quitPanel.SetActive(false);
        eventSystem.SetSelectedGameObject(playButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


