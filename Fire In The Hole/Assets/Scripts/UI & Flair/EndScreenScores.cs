using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreenScores : MonoBehaviour
{
    private GameTimer scoreTracker;

    private int[] scores;

    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI player3Score;
    public TextMeshProUGUI player4Score;

    // Start is called before the first frame update
    void Start()
    {
        scoreTracker = GameObject.FindGameObjectWithTag("Timer").GetComponent<GameTimer>();

        Debug.Log(scoreTracker.playerScores[0].ToString()) ;

        if (!string.IsNullOrEmpty(scoreTracker.playerScores[0].ToString())) player1Score.text = scoreTracker.playerScores[0].ToString();
        else player1Score.text = string.Empty;
        if (!string.IsNullOrEmpty(scoreTracker.playerScores[1].ToString())) player2Score.text = scoreTracker.playerScores[1].ToString();
        else player2Score.text = string.Empty;
        if (!string.IsNullOrEmpty(scoreTracker.playerScores[2].ToString())) player3Score.text = scoreTracker.playerScores[2].ToString();
        else player3Score.text = string.Empty;
        if (!string.IsNullOrEmpty(scoreTracker.playerScores[3].ToString())) player4Score.text = scoreTracker.playerScores[3].ToString();
        else player4Score.text = string.Empty;


        int max = 0;       // largest quantity
        int which = 0;              // who has the largest quantity

        for (int i = 0; i < scoreTracker.playerScores.Length; i++)
        {
            // take the value out for consideration
            var value = scoreTracker.playerScores[i];
            if (string.IsNullOrEmpty(value.ToString())) return;

            if (i == 0)   // first one is by default the biggest
            {
                max = value;
                which = 0;
            }

            if (value > max)   // consider each one to see if it is bigger
            {
                max = value;
                which = i;
            }
        }

        // now max has the biggest value and which is the item index that it was in

        switch (which)
        {
            case 0:
                player1Score.gameObject.GetComponentInChildren<Image>().enabled = true;
                break;
            case 1:
                player1Score.gameObject.GetComponentInChildren<Image>().enabled = true;
                break;
            case 2:
                player1Score.gameObject.GetComponentInChildren<Image>().enabled = true;
                break;
            case 3:
                player1Score.gameObject.GetComponentInChildren<Image>().enabled = true;
                break;
        }
        

    }
}