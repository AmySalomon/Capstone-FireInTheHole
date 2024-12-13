using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class EndScreenScores : MonoBehaviour
{
    private GameTimer scoreTracker;

    public int[] scores;
    private List<int> scoresOrdered = new List<int>();
    public Transform[] playerPlacements;
    public GameObject[] players;


    /*
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI player3Score;
    public TextMeshProUGUI player4Score;
    */

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < playerPlacements.Length; i++ )
        {
            playerPlacements[i].gameObject.SetActive(false);
        }

        scoreTracker = GameObject.FindGameObjectWithTag("Timer").GetComponent<GameTimer>();

        Debug.Log(scoreTracker.playerScores[0].ToString()) ;
        GetEndResults();

        //if (!string.IsNullOrEmpty(scoreTracker.playerScores[0].ToString())) player1Score.text = scoreTracker.playerScores[0].ToString();
        //else player1Score.text = string.Empty;
        //if (!string.IsNullOrEmpty(scoreTracker.playerScores[1].ToString())) player2Score.text = scoreTracker.playerScores[1].ToString();
        //else player2Score.text = string.Empty;
        //if (!string.IsNullOrEmpty(scoreTracker.playerScores[2].ToString())) player3Score.text = scoreTracker.playerScores[2].ToString();
        //else player3Score.text = string.Empty;
        //if (!string.IsNullOrEmpty(scoreTracker.playerScores[3].ToString())) player4Score.text = scoreTracker.playerScores[3].ToString();
        //else player4Score.text = string.Empty;


        //int max = 0;       // largest quantity
        //int which = 0;              // who has the largest quantity

        //for (int i = 0; i < scoreTracker.playerScores.Length; i++)
        //{
        //    // take the value out for consideration
        //    var value = scoreTracker.playerScores[i];
        //    if (string.IsNullOrEmpty(value.ToString())) return;

        //    if (i == 0)   // first one is by default the biggest
        //    {
        //        max = value;
        //        which = 0;
        //    }

        //    if (value > max)   // consider each one to see if it is bigger
        //    {
        //        max = value;
        //        which = i;
        //    }
        //}

        // now max has the biggest value and which is the item index that it was in

        //switch (which)
        //{
        //    case 0:
        //        player1Score.gameObject.GetComponentInChildren<Image>().enabled = true;
        //        break;
        //    case 1:
        //        player1Score.gameObject.GetComponentInChildren<Image>().enabled = true;
        //        break;
        //    case 2:
        //        player1Score.gameObject.GetComponentInChildren<Image>().enabled = true;
        //        break;
        //    case 3:
        //        player1Score.gameObject.GetComponentInChildren<Image>().enabled = true;
        //        break;
        //}


    }

    public void GetEndResults()
    {
        Debug.Log("Getting End Results");
        scoresOrdered.Clear();
        for(int i = 0; i < scoreTracker.players.Length; i++)
        {
            if(scoreTracker.players[i] ==null) { continue; }
            players[i] = scoreTracker.players[i];
            players[i].GetComponent<PlayerConfigInfo>().placed = false;
            scores[i] = scoreTracker.playerScores[i];
            scoresOrdered.Add(scores[i]);
            playerPlacements[i].gameObject.SetActive(true);
        }
        Debug.Log("Sorting End Results");
        scoresOrdered.Sort();
        Debug.Log("Attempting to Find First Place");
        FindFirstPlace();

    }
    public void FindFirstPlace()
    {
        Debug.Log("Finding First Place");
        int highscore = scoresOrdered[scoresOrdered.Count -1];
        Debug.Log("The score is " + highscore);
        for(int i = 0; i<scores.Length; i++)
        {
            if(scores[i] >= highscore)
            {
                Debug.Log("This player has the score!");
                playerPlacements[0].GetComponent<Image>().sprite = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite;
                players[i].GetComponent<PlayerConfigInfo>().placed = true;
                playerPlacements[0].GetComponentInChildren<TextMeshProUGUI>().text = highscore.ToString();
                break;
            }
        }

        Debug.Log("There is " + scoresOrdered.Count + " scores. Removing Score now.");
        scoresOrdered.RemoveAt(scoresOrdered.Count - 1); 
        if(scoresOrdered.Count  >= 1) //if there are still scores left to be sorted, continue down the list
        {
            Debug.Log("Finding Second Place");

            FindSecondPlace();
        }
    }

    public void FindSecondPlace()
    {
        int placementScore = scoresOrdered[scoresOrdered.Count - 1];
        for (int i = 0; i < scores.Length; i++)
        {
            if(players[i].GetComponent<PlayerConfigInfo>().placed == true) { continue; } //if the player has already been placed, skip them
            if (scores[i] >= placementScore)
            {
                playerPlacements[1].GetComponent<Image>().sprite = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite;
                players[i].GetComponent<PlayerConfigInfo>().placed = true;
                playerPlacements[1].GetComponentInChildren<TextMeshProUGUI>().text = placementScore.ToString();
                break;
            }
        }
        Debug.Log("There is " + scoresOrdered.Count + " scores. Removing Score now.");

        scoresOrdered.RemoveAt(scoresOrdered.Count - 1);
        if (scoresOrdered.Count  >= 1 )
        {
            Debug.Log("Finding Third Place");

            FindThirdPlace();
        }
    }

    public void FindThirdPlace()
    {
        int placementScore = scoresOrdered[scoresOrdered.Count - 1];
        for (int i = 0; i < scores.Length; i++)
        {
            if (players[i].GetComponent<PlayerConfigInfo>().placed == true) { continue; } //if the player has already been placed, skip them
            if (scores[i] >= placementScore)
            {
                playerPlacements[2].GetComponent<Image>().sprite = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite;
                players[i].GetComponent<PlayerConfigInfo>().placed = true;
                playerPlacements[2].GetComponentInChildren<TextMeshProUGUI>().text = placementScore.ToString();
                break;
            }
        }
        Debug.Log("There is " + scoresOrdered.Count + " scores. Removing Score now.");

        scoresOrdered.RemoveAt(scoresOrdered.Count - 1);
        if (scoresOrdered.Count  >= 1)
        {
            Debug.Log("Finding Fourth Place");

            FindFourthPlace();
        }
    }

    public void FindFourthPlace()
    {
        int placementScore = scoresOrdered[scoresOrdered.Count - 1];
        for (int i = 0; i < scores.Length; i++)
        {
            if (players[i].GetComponent<PlayerConfigInfo>().placed == true) { continue; } //if the player has already been placed, skip them
            if (scores[i] >= placementScore)
            {
                playerPlacements[3].GetComponent<Image>().sprite = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite;
                players[i].GetComponent<PlayerConfigInfo>().placed = true;
                playerPlacements[3].GetComponentInChildren<TextMeshProUGUI>().text = placementScore.ToString();
                break;
            }
        }
    }
}