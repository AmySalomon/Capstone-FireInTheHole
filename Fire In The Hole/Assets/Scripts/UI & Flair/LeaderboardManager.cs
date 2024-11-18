using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public int[] playerScores;
    public GameObject[] players;
    public Transform[] playerScoreboards;
    public static LeaderboardManager leaderboardManager;



    private void Awake()
    {
        if(leaderboardManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            leaderboardManager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void FindScoreLead()
    {
        GetPlayerScores();
        int currentHighscore = Mathf.Max(playerScores); //set the current highest score any player has to the highscore
        for (int i = 0; i < playerScores.Length; i++) //if the player's score is equal to the highscore, show the scoreleader Laurel
        {
            if (playerScoreboards[i] == null) { return; }
            if (playerScores[i] >= currentHighscore)
            {
                players[i].GetComponentInChildren<PlayerScore>().ShowScoreLeader();
            }
            else
            {
                players[i].GetComponentInChildren<PlayerScore>().HideScoreLeader();
            }
        }
        
    }

    public void GetPlayerScores()//get current player scores
    {
        for (int i = 0; i < playerScoreboards.Length; i++)
        {
            if(playerScoreboards[i] == null) { return; }
            playerScores[i] = playerScoreboards[i].gameObject.GetComponent<ScoreTracker>().score;
        }
    }
}
