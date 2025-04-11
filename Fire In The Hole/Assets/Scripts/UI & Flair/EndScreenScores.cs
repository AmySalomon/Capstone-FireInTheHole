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

    //Accolades Variables
    public List<int> deathsOrdered = new List<int>();
    public List<int> killsOrdered, shotsFiredOrdered, golfballKillsOrdered, selfDestructsOrdered, puttsTaken, puttsMissed, powerupsGained, weaponsGained = new List<int>();
    public Sprite deathsAccolade, killsAccolade, shotsFiredAccolade, golfballKillsAccolade, selfDestructsAccolade, puttsTakenAccolade, 
        puttsMissedAccolade, powerupsGainedAccolade, weaponsGainedAccolade, mostDeathsByAccolade;
    //Accolades Titles
    public string deathsTitle = "Caddie";
    public string killsTitle = "Bloodthirsty";
    public string shotsTitle = "Trigger Happy";
    public string golfballKillsTitle = "Putt Sniper";
    public string sdTitle = "Accident Prone";
    public string puttsTakenTitle = "Putting Maniac";
    public string puttsMissedTitle = "Duffer";
    public string powerupsTitle = "Power Tripping";
    public string weaponsTitle = "Weapons Junkie";
    //public string mostDeathsByTitle = " Caddie";


    public GameObject accoladeHolderFirst, accoladeHolderSecond, accoladeHolderThird, accoladeHolderFourth;
    public GameObject accolade;
    public int maxAccolades = 2; //how many accolades to show off for a player at max
    public int accoladesToAssign = 4; //how many accolades to give players
    public delegate void AssignAccolades();
    AssignAccolades assignAccolades;
    List<int> numbers = new List<int>()
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9
    };
    //public List<>
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

    }

    public void ScoreDebugLog()
    {
        string scoreList = "The Current Score List is ";
        foreach(int i in scoresOrdered)
        {
            scoreList += i.ToString()+ ", ";
        }

        //foreach(var item  in scoresOrdered)
        Debug.Log(scoreList);
    }
    public void GetEndResults()
    {
        ScoreDebugLog();
        //Debug.Log("Previous End Scores Are: " + scoresOrdered);
        //Debug.Log("Getting End Results");
        scoresOrdered.Clear();
        for(int i = 0; i < scoreTracker.players.Length; i++)
        {
            if(scoreTracker.players[i] == null) { continue; }
            players[i] = scoreTracker.players[i];
            players[i].GetComponent<PlayerConfigInfo>().placed = false;
            scores[i] = scoreTracker.playerScores[i];
            scoresOrdered.Add(scores[i]);
            playerPlacements[i].gameObject.SetActive(true);

        }
        //Debug.Log("Sorting End Results");
        scoresOrdered.Sort();
        ScoreDebugLog();
        //Debug.Log("New End Scores Are: " + scoresOrdered);
        ChooseAccolades();
        //Debug.Log("Attempting to Find First Place");
        FindFirstPlace();

    }

    //when assigning accolades, only allow two per player
    public void FindFirstPlace()
    {
        Debug.Log("Finding First Place");
        int highscore = scoresOrdered[scoresOrdered.Count - 1];
        Debug.Log("The score is " + highscore);
        for(int i = 0; i<scores.Length; i++)
        {
            if(scores[i] >= highscore)
            {
                Debug.Log("Player "+ players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite+" score of "+ scores[i]+" has the score!");
                playerPlacements[0].GetComponent<Image>().sprite = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite;
                players[i].GetComponent<PlayerConfigInfo>().placed = true;
                playerPlacements[0].GetComponentInChildren<TextMeshProUGUI>().text = highscore.ToString();

                PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
                if (stats.accolades.Count > 0)
                {
                    for(int j = 0; j < maxAccolades && j < stats.accolades.Count; j++)
                    {
                        //stats.accolades.Keys[] is used to get the key for the dictionary
                        if (stats.accoladeKeys[j] == null) { break; }

                        string key = stats.accoladeKeys[j];
                        GameObject accoladeObj = Instantiate(accolade, accoladeHolderFirst.transform, false);
                        accoladeObj.GetComponent<Image>().sprite = stats.accolades[key];
                        accoladeObj.GetComponentInChildren<TextMeshProUGUI>().text = key.ToString();
                    }
                }

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
                Debug.Log("Player " + players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite + " score of " + scores[i] + " has second!");
                playerPlacements[1].GetComponent<Image>().sprite = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite;
                players[i].GetComponent<PlayerConfigInfo>().placed = true;
                playerPlacements[1].GetComponentInChildren<TextMeshProUGUI>().text = placementScore.ToString();

                PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
                if (stats.accolades.Count > 0)
                {
                    for (int j = 0; j < maxAccolades && j < stats.accolades.Count; j++)
                    {
                        //stats.accolades.Keys[] is used to get the key for the dictionary

                        if (stats.accoladeKeys[j] == null) { break; }
                        string key = stats.accoladeKeys[j];
                        GameObject accoladeObj = Instantiate(accolade, accoladeHolderSecond.transform, false);
                        accoladeObj.GetComponent<Image>().sprite = stats.accolades[key];
                        accoladeObj.GetComponentInChildren<TextMeshProUGUI>().text = key.ToString();
                    }
                }

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
                Debug.Log("Player " + players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite + " score of " + scores[i] + " has third!");
                playerPlacements[2].GetComponent<Image>().sprite = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.ThirdPlaceSprite;
                players[i].GetComponent<PlayerConfigInfo>().placed = true;
                playerPlacements[2].GetComponentInChildren<TextMeshProUGUI>().text = placementScore.ToString();

                PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
                if (stats.accolades.Count > 0)
                {
                    for (int j = 0; j < maxAccolades && j < stats.accolades.Count; j++)
                    {
                        //stats.accolades.Keys[] is used to get the key for the dictionary
                        if (stats.accoladeKeys[j] == null) { break; }
                        string key = stats.accoladeKeys[j];
                        GameObject accoladeObj = Instantiate(accolade, accoladeHolderThird.transform, false);
                        accoladeObj.GetComponent<Image>().sprite = stats.accolades[key];
                        accoladeObj.GetComponentInChildren<TextMeshProUGUI>().text = key.ToString();
                    }
                }
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
                Debug.Log("Player " + players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.VictorySprite + " score of " + scores[i] + " has fourth!");
                playerPlacements[3].GetComponent<Image>().sprite = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.LastPlaceSprite;
                players[i].GetComponent<PlayerConfigInfo>().placed = true;
                playerPlacements[3].GetComponentInChildren<TextMeshProUGUI>().text = placementScore.ToString();

                PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
                if (stats.accolades.Count > 0)
                {
                    for (int j = 0; j < maxAccolades && j < stats.accolades.Count; j++)
                    {
                        //stats.accolades.Keys[] is used to get the key for the dictionary
                        if (stats.accoladeKeys[j] == null) { break; }

                        string key = stats.accoladeKeys[j];
                        GameObject accoladeObj = Instantiate(accolade, accoladeHolderFourth.transform, false);
                        accoladeObj.GetComponent<Image>().sprite = stats.accolades[key];
                        accoladeObj.GetComponentInChildren<TextMeshProUGUI>().text = key.ToString();
                    }
                }
                break;
            }
        }
    }

    public void ChooseAccolades()
    {
        assignAccolades = null;
        List<int> numbersInstance = numbers;
        for (int i = 0; i < accoladesToAssign; i++)
        {
            int accoladeChosen = numbersInstance[UnityEngine.Random.Range(0, numbersInstance.Count-1)];
            switch (accoladeChosen)
            {
                case 0:
                    assignAccolades += FindMostDeaths;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                case 1:
                    assignAccolades += FindMostKills;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                case 2:
                    assignAccolades += FindMostShotsFired;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                case 3:
                    assignAccolades += FindMostGolfballKills;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                case 4:
                    assignAccolades += FindMostSDs;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                case 5:
                    assignAccolades += FindMostPuttsTaken;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                case 6:
                    assignAccolades += FindMostPuttsMissed;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                case 7:
                    assignAccolades += FindMostPowerupsGained;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                case 8:
                    assignAccolades += FindMostWeaponsGained;
                    numbersInstance.Remove(accoladeChosen);
                    break;
                //case 9:
                //    assignAccolades += FindMostDeathsBy;
                //    numbersInstance.Remove(accoladeChosen);
                //    break;
            }
        }
        assignAccolades.Invoke();
    }

    public void FindMostDeaths()
    {
        deathsOrdered.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            deathsOrdered.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.deaths);
            deathsOrdered.Sort();
        }

        int highscore = deathsOrdered[deathsOrdered.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }

            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.deaths >= highscore)
            {
                stats.accolades.Add(deathsTitle, deathsAccolade);
                stats.accoladeKeys.Add(deathsTitle);
                Debug.Log("most deaths awarded to " + players[i]);

            }
        }
    }

    //public void FindMostDeathsBy() //an accolade for dying to one specific person a lot
    //{
    //    int highscore = 0;

    //}
    public void FindMostKills()
    {
        killsOrdered.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            killsOrdered.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.kills);
            killsOrdered.Sort();
        }

        int highscore = killsOrdered[killsOrdered.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }

            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.kills >= highscore)
            {
                stats.accolades.Add(killsTitle, killsAccolade);
                stats.accoladeKeys.Add(killsTitle);
                Debug.Log("most kills awarded to " + players[i]);

            }
        }
    }

    public void FindMostShotsFired()
    {
        shotsFiredOrdered.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            shotsFiredOrdered.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.shotsFired);
            shotsFiredOrdered.Sort();
        }
        int highscore = shotsFiredOrdered[shotsFiredOrdered.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }

            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.shotsFired >= highscore)
            {
                stats.accolades.Add(shotsTitle, shotsFiredAccolade);
                stats.accoladeKeys.Add(shotsTitle);
                Debug.Log("most shots fired awarded to " + players[i]);

            }
        }
    }

    public void FindMostGolfballKills()
    {
        golfballKillsOrdered.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            golfballKillsOrdered.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.golfballKills);
            golfballKillsOrdered.Sort();
        }
        int highscore = golfballKillsOrdered[golfballKillsOrdered.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }

            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.golfballKills >= highscore)
            {
                stats.accolades.Add(golfballKillsTitle, golfballKillsAccolade);
                stats.accoladeKeys.Add(golfballKillsTitle);
                Debug.Log("most golfball kills awarded to " + players[i]);

            }
        }
    }

    public void FindMostSDs()
    {
        selfDestructsOrdered.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            selfDestructsOrdered.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.selfDestructs);
            selfDestructsOrdered.Sort();
        }
        int highscore = selfDestructsOrdered[selfDestructsOrdered.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }

            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.selfDestructs >= highscore)
            {
                stats.accolades.Add(sdTitle, selfDestructsAccolade);
                stats.accoladeKeys.Add(sdTitle);
                Debug.Log("most sds awarded to " + players[i]);

            }
        }
    }

    public void FindMostPuttsTaken()
    {
        puttsTaken.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            puttsTaken.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.puttsTaken);
            puttsTaken.Sort();
        }
        int highscore = puttsTaken[puttsTaken.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.puttsTaken >= highscore)
            {
                stats.accolades.Add(puttsTakenTitle, puttsTakenAccolade);
                stats.accoladeKeys.Add(puttsTakenTitle);
                Debug.Log("most putts awarded to " + players[i]);

            }
        }
    }

    public void FindMostPuttsMissed()
    {
        puttsMissed.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            puttsMissed.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.puttsMissed);
            puttsMissed.Sort();
        }

        int highscore = puttsMissed[puttsMissed.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.puttsMissed >= highscore)
            {
                stats.accolades.Add(puttsMissedTitle, puttsMissedAccolade);
                stats.accoladeKeys.Add(puttsMissedTitle);
                Debug.Log("most putts missed awarded to " + players[i]);

            }
        }
    }

    public void FindMostPowerupsGained()
    {
        powerupsGained.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            powerupsGained.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.powerupsGained);
            powerupsGained.Sort();
        }
        int highscore = powerupsGained[powerupsGained.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.powerupsGained >= highscore)
            {
                stats.accolades.Add(powerupsTitle, powerupsGainedAccolade);
                stats.accoladeKeys.Add(powerupsTitle);
                Debug.Log("most powerups gained awarded to " + players[i]);

            }
        }
    }

    public void FindMostWeaponsGained()
    {
        weaponsGained.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            weaponsGained.Add(players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats.weaponsGained);
            weaponsGained.Sort();
        }
        int highscore = weaponsGained[weaponsGained.Count - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }
            PlayerStats stats = players[i].GetComponent<PlayerConfigInfo>().playerConfigPublic.Stats;
            if (stats.weaponsGained >= highscore)
            {
                stats.accolades.Add(weaponsTitle, weaponsGainedAccolade);
                stats.accoladeKeys.Add(weaponsTitle);
                Debug.Log("most weapons gained awarded to " + players[i]);

            }
        }
    }
}