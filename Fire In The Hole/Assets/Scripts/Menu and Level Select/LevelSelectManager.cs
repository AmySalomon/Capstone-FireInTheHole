using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectManager : MonoBehaviour
{
    //Random Play Level
    public int randomLevel;

    //Outputs chosen level name
    public string chosenLevel;

    //For Loading and Destroying
    public static LevelSelectManager LSManager;

    public string playerSetup;

    //Transition
    public Animator transition;
    public float transitionTime;
    public EventSystem eventSystem;

    private void Awake()
    {
        //No duplicate LevelSelectManagers!!!
        if (LSManager != null)
        {
            Destroy(gameObject);
            Debug.Log("[LevelSelectManager]: Destroyed LevelSelectManager.");
        }
        else
        {
            LSManager = this;
        }
        DontDestroyOnLoad(this.gameObject);

        //No duplicate PlayerManagers!!!
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "LevelSelect")
        {
            GameObject[] PMObject;
            PMObject = GameObject.FindGameObjectsWithTag("PlayerManager");
            foreach (GameObject manager in PMObject)
                Destroy(manager);
            Debug.Log("[LevelSelectManager]: Destroyed Player Manager.");

            //No duplicate Game Timers!!!
            GameObject[] GTObject;
            GTObject = GameObject.FindGameObjectsWithTag("Timer");
            foreach (GameObject timer in GTObject)
                Destroy(timer);
            Debug.Log("[LevelSelectManager]: Destroyed Game Timer.");
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

    public void BackToMain()
    {
        /*Time.timeScale = 1f;
        chosenLevel = "MainMenu";
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene("MainMenu");*/

        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void LevelRandom()
    {
        /*Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;*/

        randomLevel = Random.Range(1, 5); //Random Level Generator
        Debug.Log(randomLevel);
        if (randomLevel == 1)
        {
            chosenLevel = "3D Blacklight";
            //SceneManager.LoadScene(playerSetup);
            StartCoroutine(LoadLevel(playerSetup));
        }
        else if (randomLevel == 2)
        {
            chosenLevel = "3D Dinosaur";
            //SceneManager.LoadScene(playerSetup);
            StartCoroutine(LoadLevel(playerSetup));
        }
        else if (randomLevel == 3)
        {
            chosenLevel = "3D Volcano";
            //SceneManager.LoadScene(playerSetup);
            StartCoroutine(LoadLevel(playerSetup));
        }
        else if (randomLevel == 4)
        {
            chosenLevel = "3D Pirate";
            //SceneManager.LoadScene(playerSetup);
            StartCoroutine(LoadLevel(playerSetup));
        }
        else
        {
            Debug.LogError("LevelSelectManager: Randomizer Broke. (LevelRandom)");
        }
    }

    public void LevelBlacklight()
    {
        //Time.timeScale = 1f;
        chosenLevel = "3D Blacklight";
        /*Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);*/
        StartCoroutine(LoadLevel(playerSetup));
    }

    public void LevelDinosaur()
    {
        //Time.timeScale = 1f;
        chosenLevel = "3D Dinosaur";
        /*Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);*/
        StartCoroutine(LoadLevel(playerSetup));
    }

    public void LevelVolcano()
    {
        //Time.timeScale = 1f;
        chosenLevel = "3D Volcano";
        /*Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);*/
        StartCoroutine(LoadLevel(playerSetup));
    }
        

    public void LevelPirate()
    {
        //Time.timeScale = 1f;
        chosenLevel = "3D Pirate";
        /*Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);*/
        StartCoroutine(LoadLevel(playerSetup));
    }

    public void LevelDebug()
    {
        //Time.timeScale = 1f;
        chosenLevel = "Debug - Pirate";
        /*Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);*/
        StartCoroutine(LoadLevel(playerSetup));
    }

    public void HowToPlay()
    {
        //Time.timeScale = 1f;
        chosenLevel = "OptionsMenu";
        /*Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SceneManager.LoadScene(playerSetup);*/
        StartCoroutine(LoadLevel(playerSetup));
    }
}