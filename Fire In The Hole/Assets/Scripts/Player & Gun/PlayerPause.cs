using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Steamworks;


public class PlayerPause : MonoBehaviour
{
    public Transform pauseMenu;
    public static bool paused;
    public static int? playerPaused;

    protected Callback<GameOverlayActivated_t> overlayIsOn;
    // Start is called before the first frame update
    void Start()
    {
        playerPaused = null;
        Transform tempPause = FindAnyObjectByType<PauseMenu>().pauseMenu;
        GetPauseMenu(tempPause);

        if (SteamManager.Initialized) overlayIsOn = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
    }
    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (paused == false && IntroFlyBy.gameStarted == true)
        {
            PauseGame(0);
        }
    }

    void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }
        void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    //Whenever a controller is removed or added, pause the game if not already paused.
    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (paused == false && IntroFlyBy.gameStarted == true)
        {
            PauseGame(0);
        }
    }

    public void PauseGame(int index)
    {
        paused = true;
        if (pauseMenu != null)
        {
            pauseMenu.gameObject.SetActive(true);
        }
        playerPaused = index;

        GameObject playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        playerManager.GetComponent<PlayerInputManager>().enabled = false;
        Time.timeScale = 0;


    }

    
    public void UnpauseGame()
    {
        if(pauseMenu!= null)
        {
            pauseMenu.gameObject.SetActive(false);

        }
        GameObject playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        playerManager.GetComponent<PlayerInputManager>().enabled = true;
        playerManager.GetComponent<PlayerInputManager>().EnableJoining();
        Time.timeScale = 1f;
        paused = false;
        playerPaused = null;
    }

    public void GetPauseMenu(Transform currentPauseMenu)
    {
        //Debug.Log("currentPauseMenu is "+ currentPauseMenu);

        pauseMenu = currentPauseMenu;

        // Debug.Log("UPDATED pauseMenu is currently " + pauseMenu.name + " and currentPauseMenu is " + currentPauseMenu);

    }
}
