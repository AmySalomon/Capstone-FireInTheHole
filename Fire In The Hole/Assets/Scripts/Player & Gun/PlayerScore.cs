using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public Transform myScore;
    public Sprite mySprite;
    public Transform scoreLeaderSprite;
    public void IncreaseScore()
    {
        myScore.GetComponent<ScoreTracker>().UpdateScore();
        LeaderboardManager.leaderboardManager.FindScoreLead();
    }

    public void SetSprite()
    {
        myScore.GetComponent<ScoreTracker>().UpdateSprite(mySprite);
    }

    public void ShowScoreLeader()
    {
        scoreLeaderSprite.gameObject.SetActive(true);
    }

    public void HideScoreLeader()
    {
        scoreLeaderSprite.gameObject.SetActive(false);
    }
}
