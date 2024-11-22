using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SceneManagement;

//this script checks how many players have readied up, and starts the game if all available players have. it gets signals from each tutorial golf hole, and stores them.
public class ReadyManager : MonoBehaviour
{
    public int playersReady = 0;
    public int howManyPlayers = 0;

    private List<PlayerConfig> playerConfigs;

    //walls on the tutorial level blocking -insert player number- from leaving tutorial ready area
    public GameObject Wall1;
    public GameObject Wall2;
    public GameObject Wall3;
    public GameObject Wall4;

    public SpriteRenderer FadeOut;

    public TextMeshProUGUI CountdownText;

    private float timer;
    // Update is called once per frame
    void Update()
    {
        //gets the player config count from the joinplayer script. depending on the count, determines if the game should start or not.
        playerConfigs = JoinPlayer.Instance.GetPlayerConfigs();
        howManyPlayers = playerConfigs.Count;

        if (playersReady > 0)
        {
            if (playersReady >= howManyPlayers)
            {
                StartTheGame();
            }
        }
    }

    
    
    //the scene transition for the game to start
    public void StartTheGame()
    {
        timer += Time.deltaTime;

        if (timer > 3)
        {
            //opaque black
            FadeOut.color = new Color(0, 0, 0, 1);
            CountdownText.text = " ";
            SceneManager.LoadScene("IntoGameTransition");

        }
        else if (timer > 2)
        {
            //very dark
            FadeOut.color = new Color(0, 0, 0, .7f);
            CountdownText.text = "1";
            CountdownText.fontSize = 2;
        }
        else if (timer > 1)
        {
            //darker
            FadeOut.color = new Color(0, 0, 0, .4f);
            CountdownText.text = "2";
            CountdownText.fontSize = 1.5f;
        }
        else
        {
            //dark overlay
            FadeOut.color = new Color(0, 0, 0, .2f);
            CountdownText.text = "3";
            CountdownText.fontSize = 1.2f;
        }
    }
    //destroys the tutorial wall based on the info given by the lobby holes
    public void DestroyWall(int wall)
    {
        switch (wall)
        {
            case 1:
                Destroy(Wall1);
                break;

            case 2:
                Destroy(Wall2);
                break;

            case 3:
                Destroy(Wall3);
                break;

            case 4:
                Destroy(Wall4);
                break;
        }
    }
}
