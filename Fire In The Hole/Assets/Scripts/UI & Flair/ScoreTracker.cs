using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString()+ " POINTS";
    }
}
