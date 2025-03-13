using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public Transform playerIcon, scoreLeaderSprite;
    public int score = -1;
    // Start is called before the first frame update
    void Start()
    {
        //done to reset player scores to zero
        score = -1;
        UpdateScore(1);
    }

    public void UpdateScore(int value) //increase the player's score by value
    {
        score += value;
        Debug.Log("Increased score by " + value + ". score is now: " + score);
        scoreText.text = score.ToString()+ " POINTS";
    }

    public void UpdateSprite(Sprite mySprite)
    {
        playerIcon.GetComponent<Image>().sprite = mySprite;
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
