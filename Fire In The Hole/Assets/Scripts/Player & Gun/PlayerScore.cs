using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public Transform myScore;
    public Sprite mySprite;
    public Transform scoreLeaderSprite;
    public void IncreaseScore(int value)
    {
        Debug.Log("trying to call increase score to increase points by "+ value);
        myScore.GetComponent<ScoreTracker>().UpdateScore(value);
        LeaderboardManager.leaderboardManager.FindScoreLead();
    }

    public void SetSprite()
    {
        myScore.GetComponent<ScoreTracker>().UpdateSprite(mySprite);
    }

    public void ShowScoreLeader() //show laurel on leaderboard if player has the most points
    {
        scoreLeaderSprite.gameObject.SetActive(true);
    }

    public void HideScoreLeader() //hide laurel on leaderboard if player does not have the most points
    {
        scoreLeaderSprite.gameObject.SetActive(false);
    }
}
