using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public Transform myScore;
    public Sprite mySprite;
    public void IncreaseScore()
    {
        myScore.GetComponent<ScoreTracker>().UpdateScore();
    }

    public void SetSprite()
    {
        myScore.GetComponent<ScoreTracker>().UpdateSprite(mySprite);
    }
}
