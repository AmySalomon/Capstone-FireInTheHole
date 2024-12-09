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
        if (leaderboardManager != null)
        {
            Debug.Log("Singleton - Trying to create another instance of singleton -BAD-");
            Destroy(leaderboardManager);
        }
        
        
            leaderboardManager = this;
        
        DontDestroyOnLoad(this.gameObject);

    }
    public void FindScoreLead()
    {
        GetPlayerScores();
        int currentHighscore = Mathf.Max(playerScores); //set the current highest score any player has to the highscore
        for (int i = 0; i < playerScores.Length; i++) //if the player's score is equal to the highscore, show the scoreleader Laurel
        {
            if (playerScoreboards[i] == null) { return; }
            if (playerScores[i] >= currentHighscore) //if the player has a score equal to or greater than (somehow) the highest score, display they're in the lead, if not, hide it
            {
                
                players[i].GetComponentInChildren<PlayerScore>(true).ShowScoreLeader();
                playerScoreboards[i].gameObject.GetComponent<ScoreTracker>().ShowScoreLeader();
            }
            else
            {
                players[i].GetComponentInChildren<PlayerScore>(true).HideScoreLeader();
                playerScoreboards[i].gameObject.GetComponent<ScoreTracker>().HideScoreLeader();

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

    public void HidePlayers()
    {

    }
}
