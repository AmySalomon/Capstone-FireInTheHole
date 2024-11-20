using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script checks how many players have readied up, and starts the game if all available players have. it gets signals from each tutorial golf hole, and stores them.
public class ReadyManager : MonoBehaviour
{
    public int playersReady = 0;
    public int howManyPlayers = 0;

    private List<PlayerConfig> playerConfigs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                JoinPlayer.Instance.GoToGameScene();
            }
        }
    }
}
