using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    public Transform pauseMenu;
    public static bool paused;
    public static int? playerPaused;
    // Start is called before the first frame update
    void Start()
    {
        playerPaused = null;
        Transform tempPause = FindAnyObjectByType<PauseMenu>().pauseMenu;
        GetPauseMenu(tempPause);
    }

    public void PauseGame(int index)
    {
        playerPaused = index;
        paused = true;
        Time.timeScale = 0;
        if(pauseMenu!= null)
        {
            pauseMenu.gameObject.SetActive(true);
        }

    }

    
    public void UnpauseGame()
    {
        if(pauseMenu!= null)
        {
            pauseMenu.gameObject.SetActive(false);

        }
        Time.timeScale = 1f;
        paused = false;
        playerPaused = null;
    }

    public void GetPauseMenu(Transform currentPauseMenu)
    {
        Debug.Log("currentPauseMenu is "+ currentPauseMenu);
        
        pauseMenu = currentPauseMenu;

        Debug.Log("UPDATED pauseMenu is currently " + pauseMenu.name + " and currentPauseMenu is " + currentPauseMenu);

    }
}
