using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashTransition : MonoBehaviour
{
    //this script advances through the splash screen
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 9.5)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
