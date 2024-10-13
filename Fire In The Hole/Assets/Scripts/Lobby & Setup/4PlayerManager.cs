using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourPlayerManager : MonoBehaviour
{
    public int deadPlayerCount = 3;

    public int currentPlayersDead = 0;

    private float resetTimer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayersDead >= deadPlayerCount)
        {
            resetTimer += Time.deltaTime;

            if (resetTimer >= 3)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
