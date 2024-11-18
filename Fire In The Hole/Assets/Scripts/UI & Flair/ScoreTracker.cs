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
        score = -1;
        UpdateScore();
    }

    public void UpdateScore()
    {
        score++;
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
