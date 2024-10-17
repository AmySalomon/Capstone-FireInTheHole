using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public Transform myScore;

    public void IncreaseScore()
    {
        myScore.GetComponent<ScoreTracker>().UpdateScore();
    }
}
